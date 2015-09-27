<#
* Copyright (c) Inish Technology Ventures Limited.  All rights reserved.
*
* This code is licensed under the BSD 3-Clause License included with this source
*
* ALSO SEE: https://github.com/SoftwarePotential/samples/wiki/License
#>

function Import-PfxCertificate {
	Param (
		 [string] $pfx,
		 [string] $password,
		 [string] $containerName
	);
	process {
		if(!(Test-Path -Path $pfx)) {
			Write-Error "Unable to locate PFX file: $pfx";
			return;
		}

		$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2;
		$cert.Import($pfx, $password, [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]::Exportable);
		$exportPrivateKeyInformation = $true;
		$certXml = $cert.PrivateKey.ToXmlString($exportPrivateKeyInformation);

		$csp = New-Object System.Security.Cryptography.CspParameters;
		$csp.KeyContainerName = $containerName;
		$csp.Flags = [System.Security.Cryptography.CspProviderFlags]::UseMachineKeyStore -bor [System.Security.Cryptography.CspProviderFlags]::NoPrompt; # -bor is biwise or
		$csp.KeyNumber = [System.Security.Cryptography.KeyNumber]::Signature;

		$rsa = New-Object System.Security.Cryptography.RSACryptoServiceProvider $csp;
		$rsa.FromXmlString($certXml);
		$rsa.Clear();

		"Sucesfully imported $pfx into StrongName CSP store";
	}
}

Export-ModuleMember -Function Import-PfxCertificate;