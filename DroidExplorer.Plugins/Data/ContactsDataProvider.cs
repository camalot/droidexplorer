using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Plugins.Contacts;
using System.Data.SQLite;
using DroidExplorer.Core;
using System.Drawing;
using System.IO;

namespace DroidExplorer.Plugins.Data {
  public class ContactsDataProvider : SqliteDataProvider {
    private const string DATABASE_FILE = "/data/data/com.android.providers.contacts/databases/contacts.db";

    private const string GETPEOPLE_SQL = "SELECT * FROM people p1 INNER JOIN photos p2 ON p1._id = p2.person";
    private const string GETPERSONPHONES_SQL = "SELECT * FROM phones WHERE person = {0}";

    public ContactsDataProvider ( )
      : base ( DATABASE_FILE ) {

    }

    public List<Contact> GetContacts ( ) {
      List<Contact> contacts = new List<Contact> ( );
      using ( SQLiteDataReader reader = ExecuteReader ( GETPEOPLE_SQL ) ) {
        while ( reader.Read ( ) ) {
          Contact contact = new Contact ( );
          contact.ID = ( long )reader[ "_id" ];
          contact.SyncAccount = ( string )reader[ "_sync_account" ];
          contact.SyncDirty = reader[ "_sync_dirty" ] == DBNull.Value ? false : ( long )reader[ "_sync_dirty" ] == 1;
          contact.SyncID = reader[ "_sync_id" ] == DBNull.Value ? string.Empty : ( string )reader[ "_sync_id" ];
          contact.SyncLocalID = reader[ "_sync_local_id" ] == DBNull.Value ? string.Empty : ( string )reader[ "_sync_local_id" ];
          contact.SyncMark = reader[ "_sync_mark" ] == DBNull.Value ? false : ( long )reader[ "_sync_mark" ] == 1;
          contact.SyncTime = reader[ "_sync_time" ] == DBNull.Value ? -1 : long.Parse ( ( string )reader[ "_sync_time" ] );
          contact.SyncVersion = reader[ "_sync_version" ] == DBNull.Value ? -1 : long.Parse ( ( string )reader[ "_sync_version" ] );
          contact.CustomRingtone = reader[ "custom_ringtone" ] == DBNull.Value ? false : ( long )reader[ "custom_ringtone" ] == 1;
          contact.IsStarred = reader[ "starred" ] == DBNull.Value ? false : ( long )reader[ "starred" ] == 1;
          contact.LastTimeContacted = reader[ "last_time_contacted" ] == DBNull.Value ? -1 : ( long )reader[ "last_time_contacted" ];
          contact.Name = reader[ "name" ] == DBNull.Value ? string.Empty : ( string )reader[ "name" ];
          contact.Notes = reader[ "notes" ] == DBNull.Value ? string.Empty : ( string )reader[ "notes" ];
          contact.PhoneticName = reader[ "phonetic_name" ] == DBNull.Value ? string.Empty : ( string )reader[ "phonetic_name" ];
          contact.PhotoVersion = reader[ "photo_version" ] == DBNull.Value ? -1 : ( long )reader[ "photo_version" ];
          contact.PrimaryEmail = reader[ "primary_email" ] == DBNull.Value ? -1 : ( long )reader[ "primary_email" ];
          contact.PrimaryOrganization = reader[ "primary_organization" ] == DBNull.Value ? -1 : ( long )reader[ "primary_organization" ];
          contact.PrimaryPhone = reader[ "primary_phone" ] == DBNull.Value ? -1 : ( long )reader[ "primary_phone" ];
          contact.SendToVoiceMail = reader[ "send_to_voicemail" ] == DBNull.Value ? false : ( long )reader[ "send_to_voicemail" ] == 1;
          contact.TimesContacted = reader[ "times_contacted" ] == DBNull.Value ? -1 : ( long )reader[ "times_contacted" ];
          if ( reader["data"] != DBNull.Value ) {
            byte[] buffer = (byte[]) reader["data"];
            using ( MemoryStream ms = new MemoryStream ( buffer, false ) ) {
              contact.Photo = Image.FromStream ( ms, true );
            }
          }
          using ( SQLiteDataReader reader2 = ExecuteReader ( string.Format ( GETPERSONPHONES_SQL, contact.ID ) ) ) {
            while ( reader2.Read ( ) ) {
              Phone phone = new Phone ( );

              phone.ID = ( long )reader2[ "_id" ];
              phone.PersonID = contact.ID;
              phone.Type = reader2[ "type" ] == DBNull.Value ? Phone.PhoneType.CUSTOM : ( Phone.PhoneType )((long)reader2[ "type" ]);
              phone.Number = reader2[ "number" ] == DBNull.Value ? string.Empty : ( string )reader2[ "number" ];
              phone.Key = reader2[ "number_key" ] == DBNull.Value ? string.Empty : ( string )reader2[ "number_key" ];
              phone.Label = reader2[ "label" ] == DBNull.Value ? string.Empty : ( string )reader2[ "label" ];
              phone.IsPrimary = reader2[ "isprimary" ] == DBNull.Value ? false : ( long )reader2[ "isprimary" ] == 1;

              contact.Phones.Add ( phone );
            }
          }
          contacts.Add ( contact );
        }
      }
      return contacts;
    }
  }
}
