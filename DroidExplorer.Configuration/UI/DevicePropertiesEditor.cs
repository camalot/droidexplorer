using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Collections;
using DroidExplorer.Core;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using Camalot.Common.Extensions;

namespace DroidExplorer.Configuration.UI {
  public class DevicePropertiesEditor : PropertyGridEditor {

    public DevicePropertiesEditor ( string device )
      : base ( ) {
      Device = device;
      
      Properties = CommandRunner.Instance.GetProperties ( device );
      SetSourceObject ( CreateObject ( ) );
    }

    private string Device { get; set; }
    private PropertyList Properties { get; set; }

    public override void SetSourceObject ( object obj ) {
      base.SetSourceObject ( obj );
    }


    /// <summary>
    /// Creates a dynamic object that is used to hold the properties that the device contains.
    /// </summary>
    /// <returns>object</returns>
    private object CreateObject ( ) {
      object obj = new object ( );
      AssemblyName assemblyName = new AssemblyName ( );
      assemblyName.Name = Device.Slug();
      AssemblyBuilder assembly = Thread.GetDomain ( ).DefineDynamicAssembly ( assemblyName, AssemblyBuilderAccess.Run );
      ModuleBuilder module = assembly.DefineDynamicModule ( string.Format ( CultureInfo.InvariantCulture, "{0}Module", Device ) );
      TypeBuilder builder = module.DefineType ( "DeviceProperties", TypeAttributes.Public | TypeAttributes.Class );

      foreach ( var item in Properties.Keys ) {
        string propertyName = item.Replace ( ".", "_" );
        FieldBuilder field = builder.DefineField ( string.Format ( "_{0}", propertyName ), typeof ( string ), FieldAttributes.Private );

        PropertyBuilder property = builder.DefineProperty ( propertyName, PropertyAttributes.None, typeof ( string ), new Type[ ] { typeof ( string ) } );

        ConstructorInfo ci = typeof ( DisplayNameAttribute ).GetConstructor ( new Type[ ] { typeof ( string ) } );
        CustomAttributeBuilder cab = new CustomAttributeBuilder ( ci, new object[ ] { item } );
        property.SetCustomAttribute ( cab );
       
        ci = typeof ( ReadOnlyAttribute ).GetConstructor ( new Type[] { typeof(bool) } );
        cab = new CustomAttributeBuilder ( ci, new object[ ] { true } );
        property.SetCustomAttribute ( cab );

        ci = typeof ( CategoryAttribute ).GetConstructor ( new Type[ ] { typeof ( string ) } );
        cab = new CustomAttributeBuilder ( ci, new object[ ] { "Device Properties" } );
        property.SetCustomAttribute ( cab );

        MethodAttributes getsetAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;

        MethodBuilder getMethod = builder.DefineMethod ( "get_value", getsetAttributes, typeof ( string ), Type.EmptyTypes );

        ILGenerator getIL = getMethod.GetILGenerator ( );
        getIL.Emit ( OpCodes.Ldarg_0 );
        getIL.Emit ( OpCodes.Ldfld, field );
        getIL.Emit ( OpCodes.Ret );

        MethodBuilder setMethod = builder.DefineMethod ( "set_value", getsetAttributes, null, new Type[ ] { typeof ( string ) } );
        ILGenerator setIL = setMethod.GetILGenerator ( );
        setIL.Emit ( OpCodes.Ldarg_0 );
        setIL.Emit ( OpCodes.Ldarg_1 );
        setIL.Emit ( OpCodes.Stfld, field );
        setIL.Emit ( OpCodes.Ret );

        property.SetGetMethod ( getMethod );
        property.SetSetMethod ( setMethod );
      }

      Type generatedType = builder.CreateType ( );
      object generatedObject = Activator.CreateInstance ( generatedType );
      PropertyInfo[] properties = generatedType.GetProperties ( );
      foreach ( var item in properties ) {

        object[] attributes = item.GetCustomAttributes ( true );
        DisplayNameAttribute dna = new DisplayNameAttribute ( item.Name.Replace ( "_", "." ) );
        foreach ( var aitem in attributes ) {
          if ( aitem is DisplayNameAttribute ) {
            dna = aitem as DisplayNameAttribute;
          }
        }

        string value = Properties[ dna.DisplayName ];
        item.SetValue ( generatedObject, value, null );
      }

      return generatedObject;
    }
  }
}
