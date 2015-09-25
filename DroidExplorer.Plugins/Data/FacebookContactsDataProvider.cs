using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Plugins.Contacts;
using System.Data.SQLite;
using System.Drawing;
using System.IO;

namespace DroidExplorer.Plugins.Data {
  public class FacebookContactsDataProvider : SqliteDataProvider {
    private const string DATABASE_FILE = "/data/data/com.facebook.katana/databases/fb.db";

    private const string GETPEOPLE_SQL = "SELECT * FROM contact_info c INNER JOIN phone_numbers p ON c.user_id = p.user_id";

    public FacebookContactsDataProvider ( )
      : base ( DATABASE_FILE ) {

    }

    public List<Contact> GetContacts ( ) {
      List<Contact> contacts = new List<Contact> ( );
      using ( SQLiteDataReader reader = ExecuteReader ( GETPEOPLE_SQL ) ) {
        while ( reader.Read ( ) ) {
          Contact contact = new Contact ( );
          if ( reader[ "user_name" ] != DBNull.Value ) {
            contact.Name = reader[ "user_name" ] == DBNull.Value ? string.Empty : ( string )reader[ "user_name" ];
            //contact.Notes = string.Format ( "facebook:{0}", reader[ "user_id" ] == DBNull.Value ? "-1" : (( long )reader[ "user_id" ]).ToString() );

            if ( reader[ "cell" ] != DBNull.Value ) {
              Phone phone = new Phone ( );
              phone.Type = Phone.PhoneType.CUSTOM;
              phone.Label = "Facebook:Cell";
              phone.Number = ( string )reader[ "cell" ];
              phone.Key = ( string )reader[ "cell" ];
              phone.IsPrimary = reader[ "other" ] == DBNull.Value;
              contact.Phones.Add ( phone );
            }

            if ( reader[ "other" ] != DBNull.Value ) {
              Phone phone = new Phone ( );
              phone.Type = Phone.PhoneType.CUSTOM;
              phone.Label = "Facebook:Other";
              phone.Number = ( string )reader[ "other" ];
              phone.Key = ( string )reader[ "other" ];
              phone.IsPrimary = reader[ "cell" ] == DBNull.Value;
              contact.Phones.Add ( phone );
            }

            if ( reader[ "user_image" ] != DBNull.Value ) {
              byte[] buffer = ( byte[ ] )reader[ "user_image" ];
              using ( MemoryStream ms = new MemoryStream ( buffer, false ) ) {
                contact.Photo = Image.FromStream ( ms, true );
              }
            }
          }
          contacts.Add ( contact );
        }
      }
      return contacts;
    }


  }
}
