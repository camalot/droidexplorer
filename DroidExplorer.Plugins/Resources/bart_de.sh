#!/bin/sh
VER="1.1.0"
MYFOLDER="bart"
SDCARD="/sdcard"
EXTPART="/system/sd"
NANDSH="nandroid-mobile.sh"
NANDLOG="nandroid.log"
CONFIGFILE="/system/sd/bart.config"

alias _GREP_="busybox grep"
alias _AWK_="busybox awk"
alias _MOUNT_="busybox mount"
alias _UMOUNT_="busybox umount"
alias _LS_="busybox ls"
alias _TAR_="busybox tar"
alias _CP_="busybox cp"
alias _RM_="busybox rm"
alias _MKDIR_="busybox mkdir"

# global variables
base=0
romname=
app_s=0
nandroid_only=0
ext_only=0
compressed=0
nand_args=
verbose=0
myecho=":"
ni=0
reboot=0
shutdown=0

do_exit()
{
	code=$1
	if [ $nandroid_only -eq 0 ]
	then
		_UMOUNT_ ${EXTPART} > /dev/null 2>&1
	fi
	_UMOUNT_ ${SDCARD} > /dev/null 2>&1
	exit $code
}

print_usage()
{
	echo ""
	echo "Usage: $0 [--noninteractive] [OPTIONS] -s|--store [rom_name]"
	echo "     : $0 [--noninteractive] [OPTIONS] -r|--restore [rom_name]"
	echo "     : $0 [--noninteractive] [-e|-n] -d|--delete [rom_name]"
	echo ""
	echo "Options are:"
	echo "	  -a --app_s, nandroid app_s dalvik_cache"
	echo " 	  -b --base, nandroid apps dalvik_cache"
	echo "          -c --compress, only used with -s"
	echo "	  -e --ext_only, use with -r or -s"
	echo "          -h --help"
	echo "          -l --list"
	echo "	  -n --nandroid_only, use with -r or -s"
	echo "          -v --version"
	echo "	  --verbose, verbose output during nandroid"
	echo "	  --norecovery, ignore recovery partition"
	echo "	  --noboot, ignore boot partition"
	echo "	  --nodata, ignore data partition"
	echo "	  --nosystem, ignore system partition"
	echo "	  --noninteractive, ignore system partition"
	echo "	  --reboot, reboot system when done"
	echo "	  --shutdown, shutdown system when done"
	echo ""
	echo "Always specify options as separate words"
	echo "  e.g. -c -r instead of -cr. Its required!"
	echo "-r and -s need to be last or followd by rom_name"
	echo "Don't use blanks or special characters in rom_name."
	echo "Compress will take longer but image will be smaller."
	echo ""
}

chooseROM()
{
	if [ $ni -eq 0 ]
	then
		listr=`_LS_ "${myhome}"`
		j=1
		for i in $listr
		do
			echo "${j}. ${i}"
			j=$((j+1))
		done
		count=$((j-1))
		echo -n "Choose a ROM (type in the number and press enter)..."
		read response
		if [ $response -gt $count -o $response -lt 1 ]
		then
			echo ""
			echo "Out of range input. You chose $response."
			echo "Choose a number in range next time...;) BYE."
			echo ""
			do_exit 41
		fi
		j=1
		for i in $listr
		do
			if [ $j -eq $response ]
			then
				romname=$i
				break;
			fi
			j=$((j+1))
		done
	else
		romname="defaultrom"
	fi
}
read_rom()
{
	if [ $ni -eq 0 ]
	then
		echo "Please entere name for ROM."
		read romname
		if [ -z "${romname}" ]
		then
			print_usage
			exit 1
		fi
	else
		romname="defaultrom"
	fi
}

# make sure /system/sd and /sdcard are mounted
$myecho "checking mount points..."
for i in ${SDCARD} ${EXTPART}
do
	if [ "$i" == ${EXTPART} ] && [ $nandroid_only -eq 1 ]
	then
		$myecho "nandroid_only chosed ext partition not needed..."
	else
		$myecho "checking whether $i is mounted..."
		mounted=`_GREP_ $i /proc/mounts | _AWK_ '{print $2}'`
		if [ "$mounted" != "$i" ]
		then
			$myecho "$i is not mounted.  mounting $i now..."
			# not mounted, mount it
			_MOUNT_ $i
			if [ $? -ne 0 ]
			then
				echo "Unable to mount $i ..."
				echo ""
				do_exit 5
			fi
		fi
	$myecho "$i is mounted..."
	fi
done


# args processing
if [ $# -lt 1 ]
then
	print_usage
	exit 1
fi

# check for config file
if [ -f $CONFIGFILE ]
then
	$myecho "found $CONFIGFILE"

	. $CONFIGFILE

fi


# get args from command line
while [ -n "$1" ]
do
	case "$1" in
	--noninteractive)
		$myecho "setting noninteractive mode..."
		ni=1
		shift
		;;
	--norecovery)
		$myecho "ignoring recovery partition..."
		nand_args="$nand_args $1"
		shift
		;;
	--noboot)
		$myecho "ignoring boot partition..."
		nand_args="$nand_args $1"
		shift	
		;;
	--nodata)
		$myecho "ignoring data partition..."
		nand_args="$nand_args $1"
		shift	
		;;
	--nosystem)
		$myecho "ignoring system partitin..."
		nand_args="$nand_args $1"
		shift	
		;;
	--verbose)
		verbose=1
		myecho="echo"
		$myecho "setting verbose mode..."
		shift	
		;;
	--reboot)
                $myecho "setting reboot option..."
                reboot=1
                shift
                ;;

	--shutdown)
                $myecho "setting shutdown option..."
                shutdown=1
                shift
                ;;
	-a|--app_s)
		$myecho "setting app_s option..."
		app_s=1
		if [ $base -eq 1 ]
		then
			print_usage
			exit 0
		fi
		shift
		;;
	-b|--base)
		$myecho "setting base option..."
		base=1
		if [ $app_s -eq 1 ]
		then
			print_usage
			exit 0
		fi
		shift
		;;
	-h|--help)
		$myecho "setting help option..."
		print_usage
		exit 0
		;;
	-c|--compress)
		$myecho "setting compress option..."
		compressed=1
		shift
		;;
	-d|--delete)
		$myecho "setting delete option..."
		cmd="delete"
		shift
		romname="$1"
		if [ -n "$romname" ]
		then
			shift
		fi
		;;
	-e|--ext_only)
		$myecho "setting ext_only option..."
		ext_only=1
		shift
		if [ $nandroid_only -eq 1 ]
		then
			print_usage
			exit 0
		fi
		;;
	-l|--list)
		$myecho "setting list option..."
		cmd="list"
		shift
		romname="$1"
		if [ -n "$romname" ]
		then
			shift
		fi
		;;
	-n|--nandroid_only)
		$myecho "setting nandroid_only option"
		nandroid_only=1
		shift
		if [ $ext_only -eq 1 ]
		then
			print_usage
			exit 0
		fi
		;;
	-r|--restore)
		$myecho "setting restore option..."
		cmd="restore"
		shift
		romname="$1"
		if [ -n "$romname" ]
		then
			shift
		fi
		;;
	-s|--store)
		$myecho "setting store option..."
		cmd="store"
		shift
		romname="$1"
		if [ -z "$romname" ]
		then
			read_rom
		fi
		shift
		;;
	-v|--version)
		$myecho "setting version option..."
		echo ""
		echo "$0 Version $VER"
		echo ""
		exit 0
		;;
	*)
		print_usage
		exit 1
	esac
done


myhome="${SDCARD}/${MYFOLDER}"
rompath="${myhome}/${romname}"
nandfolder="${rompath}/nandroid"

# what are we doing?
case "$cmd" in
	delete)
		# check for romname
		if [ -z "${romname}" ]
		then
			# no romname prompt user
			chooseROM
		fi
		rompath="${myhome}/${romname}"
		nandfolder="${rompath}/nandroid"

		if [ $nandroid_only -eq 1 ]
		then
			echo ""
			if [ $ni -eq 0 ]
			then
				echo -n "Deleting ${romname} nandroid backup, proceed? (y/n) "
				read response
			else
				response="y"
			fi
			echo ""
			if [ "$response" == "y" ]
			then
				$myecho "removing ${nandfolder}..."
				_RM_ -rf "${nandfolder}"
			else
				echo ""
				echo "Not deleting ${romname}..."
				echo ""
				do_exit 0
			fi

		elif [ $ext_only -eq 1 ]
		then
			echo ""
			if [ $ni -eq 0 ]
			then
				echo -n "Deleting ${romname} ext-backup, proceed? (y/n) "
				read response
			else
				response=y
			fi
			echo ""
			if [ "$response" == "y" ]
			then
				if [ -e "${rompath}/ext-backup.tar.gz" ] || [ -e "${rompath}/ext-backup.tar" ]
				then
	   				$myecho "removing ${rompath}/ext-backup..."		
					_RM_ "${rompath}/ext-backup"*
				else
					echo ""
					echo "ext-backup not found for ROM ${romname}."
					echo ""
					do_exit 29
				fi
			else
				echo ""
				echo "Not deleting ${romname} ext-backup..."
				echo ""
				do_exit 0
			fi


		else
			echo ""
			if [ $ni -eq 0 ]
			then
				echo -n "Deleting ${romname}, proceed? (y/n) "
				read response
			else
				response="y"
			fi
			echo ""
			if [ "$response" == "y" ]
			then
				$myecho "removing ${rompath}..."
				_RM_ -rf "${rompath}"
			else
				echo ""
				echo "Not deleting ${romname}..."
				echo ""
				do_exit 0
			fi
		fi
		;;
	list)
		_LS_ "${rompath}"
		do_exit 0
		;;
	restore)
		$myecho "checking for nandroid script..."
		if [ ! -x /sbin/nandroid-mobile.sh ]
		then
			echo ""
			echo "Script /sbin/nandroid-mobile.sh not found"
			echo "Make sure you are running compatible recovery image."
			echo ""
			do_exit 31
		fi
		
		$myecho "checking power levels..."
		ENERGY=`cat /sys/class/power_supply/battery/capacity`
		if [ "`cat /sys/class/power_supply/battery/status`" == "Charging" ]
		then
		 	ENERGY=100
		fi
		if [ ! $ENERGY -ge 30 ]
		then 
			echo "Error: not enough battery power" 
			echo "Connect charger or USB power and try again" 
			exit 1 
		fi
		
		if [ -z "${romname}" ]
		then
			chooseROM
		fi
		rompath="${myhome}/${romname}"
		nandfolder="${rompath}/nandroid"
		echo ""
		if [ $ni -eq 0 ]
		then
			echo -n "Restoring ROM ${romname}, proceed? (y/n) "
			read response
		else
			response="y"
		fi
		echo ""
		if [ "$response" != "y" ]
		then
			echo ""
			echo "Not restoring ${romname}..."
			echo ""
			do_exit 0
		fi
		if [ ! -d "${rompath}" ]
		then
			echo ""
			echo "ROM ${romname} not found at ${myhome}..."
			echo ""
			do_exit 30
		fi

		if [ $nandroid_only -eq 0 ]
		then
			compressed=0
			$myecho "checking for ext partition backup..."
			if [ -e "${rompath}/ext-backup.tar.gz" ]
			then
				$myecho "compressed ext-backup found..."
				compressed=1
			else
				if [ ! -e "${rompath}/ext-backup.tar" ]
				then
					echo ""
					echo "ext-backup not found for ROM ${romname}."
					echo ""
					do_exit 29
				fi
				$myecho "uncompressed ext-backup found..."
			fi

			# proceed with the restoration process
			echo "Cleaning up /system/sd ..."
			if [ $base -eq 1 ]
			then
				$myecho "deleting ext-base (app*, dalv*)..."
				_RM_ -rf ${EXTPART}/app* ${EXTPART}/dalv* > /dev/null 2>&1
			elif [ $app_s -eq 1 ]
			then
				$myecho "deleting app_s and dalvik-cache..."
				if [ -e ${EXTPART}/app_s ]
				then
					_RM_ -rf ${EXTPART}/app_* ${EXTPART}/dalv* > /dev/null 2>&1				
				else
					_RM_ -rf ${EXTPART}/dalv* > /dev/null 2>&1				
				fi		
			else
				$myecho "deleting  entire ext-partition..."
				_RM_ -rf ${EXTPART}/* > /dev/null 2>&1
			fi

			if [ $compressed -eq 1 ]
			then
				echo "Restoring compressed ext-backup in ${EXTPART} ..."
				cd ${EXTPART}
				gzip -c -d "${rompath}/ext-backup.tar.gz" | _TAR_ xpf -
				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during restoration of app data..."
					echo "tar/gzip operation failed."
					echo "Do you have enough space on the /sdcard?"
					echo ""
					do_exit 28
				fi
				cd /
			else
				echo "Restoring ext-backup in ${EXTPART} ..."
				cd ${EXTPART}
				_TAR_ xpf "${rompath}/ext-backup.tar"
				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during restoration of ext-backup ..."
					echo "tar operation failed."
					echo "Do you have enough space on the /sdcard?"
					echo ""
					do_exit 27
				fi
				cd /
			fi
		fi

		if [ $ext_only -eq 0 ]
		then
			# do the nandroid restore
			echo "Restoring nandroid backup..."
			_UMOUNT_ ${SDCARD}
			if [ $verbose -eq 0 ]
			then
				${NANDSH} --defaultinput $nand_args -r -p "${nandfolder}" -s "${romname}" > "${EXTPART}/${NANDLOG}" 2>&1
			else
				${NANDSH} --defaultinput $nand_args -r -p "${nandfolder}" -s "${romname}" 2>&1
			fi
			if [ $? -ne 0 ]
			then
				echo ""
				echo "Error occurred during nandroid restore..."
				echo "Have a look at \"${EXTPART}/${NANDLOG}\" to find the cause."
				echo ""
				do_exit 26
			fi
			if [ $verbose -eq 0 ]
			then
				_RM_ "${EXTPART}/${NANDLOG}"
			fi
		fi
	
		echo "Done restoring the ROM $romname."
		echo ""

		if [ $reboot -eq 1 ]
		then
			$myecho "rebooting now..."
			reboot
		fi
		if [ $shutdown -eq 1 ]
		then
			$myecho "shutting down now..."
			reboot -p
		fi
		do_exit 0
		;;
	store)
		$myecho "checking for nandroid script..."
		if [ ! -x /sbin/nandroid-mobile.sh ]
		then
			echo ""
			echo "Script /sbin/nandroid-mobile.sh not found"
			echo "Are you sure you have compatible recovery image?"
			echo ""
			do_exit 31
		fi
		echo ""
		if [ $ni -eq 0 ]
		then
			echo -n "Storing ROM ${romname}, proceed? (y/n) "
			read response
		else
			response="y"
		fi
		if [ "$response" != "y" ]
		then
			echo ""
			echo "Not storing ${romname}..."
			echo ""
			do_exit 0
		fi
		if [ ! -d "${rompath}" ]
		then
			mkdir -p "${nandfolder}"
			if [ $? -ne 0 ]
			then
				echo ""
				echo "Unable to create dir $rompath..."
				echo ""
				do_exit 14
			fi
		fi

		if [ $compressed -eq 1 ]
		then
			if [ $nandroid_only -eq 0 ]
			then
				# store the apps partition data
				echo "Storing compressed app data..."
				cd ${EXTPART}
				if [ $base -eq 1 ]
				then
					_TAR_ cpf - `_LS_ -d app* dalv* 2>/dev/null` | gzip -1 > "${rompath}/ext-backup.tar.gz"
				elif [ $app_s -eq 1 ]
				then
					_TAR_ cpf - `_LS_ -d app_* dalv* 2>/dev/null` | gzip -1 > "${rompath}/ext-backup.tar.gz"
				else
					_TAR_ cpf - `_LS_ -d * 2>/dev/null` | gzip -1 > "${rompath}/ext-backup.tar.gz"
				fi
				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during storing of app data..."
					echo "tar/gzip operation failed."
					echo "Do you have enough space on the /sdcard?"
					echo ""
					do_exit 15
				fi
			fi
			cd /
			if [ $ext_only -eq 0 ]
			then
				# do a nandroid backup
				#remove previous if exists
				$myecho "removing previous nandroid backup if it exits..."
				_RM_ -rf ${nandfolder}/*
				echo "Storing compressed nandroid backup..."
				$myecho "unmounting /sdcard for nandroid compatibility..."
				_UMOUNT_ ${SDCARD}
				if [ $verbose -eq 0 ]
				then
					${NANDSH} $nand_args -c -b -p "${nandfolder}" -s "${romname}" > "${EXTPART}/${NANDLOG}" 2>&1
				else
					${NANDSH} $nand_args -c -b -p "${nandfolder}" -s "${romname}" 2>&1
				fi

				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during nandroid backup..."
					echo "Have a look at \"${EXTPART}/${NANDLOG}\" to find the cause."
					echo ""
					do_exit 17
				fi
				if [ $verbose -eq 0 ]
				then
					_RM_ "${EXTPART}/${NANDLOG}"
				fi
			fi
		else
			if [ $nandroid_only -eq 0 ]
			then
				# store the apps partition data
				echo "Storing app data..."
				cd ${EXTPART}
				if [ $base -eq 1 ]
				then
					_TAR_ cpf "${rompath}/ext-backup.tar" `_LS_ -d app* dalv* 2>/dev/null`
				elif [ $base -eq 1 ]
				then
					_TAR_ cpf "${rompath}/ext-backup.tar" `_LS_ -d app_* dalv* 2>/dev/null`
				else
					_TAR_ cpf "${rompath}/ext-backup.tar" `_LS_ -d * 2>/dev/null`
				fi
				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during storing of app data..."
					echo "tar operation failed."
					echo "Do you have enough space on the /sdcard?"
					echo ""
					do_exit 16
				fi
			fi
			cd /
			if [ $ext_only -eq 0 ]
			then
					# do a nandroid backup
				# remove previous if exists
				$myecho "removing previous nandroid backup if it exits..."
				_RM_ -rf ${nandfolder}/*
				echo "Storing nandroid backup..."

				# I need to umount sdcard because nandroid fails if its already mounted
				# Talk of robustness...:)
				$myecho "unmounting /sdcard for nandroid compatibility..."
				_UMOUNT_ ${SDCARD}
				if [ $verbose -eq 0 ]
				then
					${NANDSH} $nand_args -b -p "${nandfolder}" -s "${romname}" > "${EXTPART}/${NANDLOG}" 2>&1
				else
					${NANDSH} $nand_args -b -p "${nandfolder}" -s "${romname}" 2>&1
				fi

				if [ $? -ne 0 ]
				then
					echo ""
					echo "Error occurred during nandroid backup..."
					echo "Have a look at \"${EXTPART}/${NANDLOG}\" to find the cause."
					echo ""
					do_exit 18
				fi
				if [ $verbose -eq 0 ]
				then
					_RM_ "${EXTPART}/${NANDLOG}"
				fi
			fi
		fi
	
		echo "Done bookmarking the ROM $romname."
		echo ""
		
		if [ $reboot -eq 1 ]
		then
			$myecho "rebooting now..."
			reboot
		fi
		if [ $shutdown -eq 1 ]
		then
			$myecho "shutting down now..."
			reboot -p
		fi
		do_exit 0
		;;
	*)
		echo ""
		echo "Unknown command \"$cmd\"..."
		print_usage
		do_exit 6
		;;
esac
