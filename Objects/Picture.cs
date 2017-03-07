using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OnlineStore.Objects
{
    public class Picture
    {
        private int _id;
        private string _pictureKey;
        private string _altText;

        public Picture(string newPictureKey, string newAltText, int newId = 0)
        {
            _id = newId;
            _pictureKey = newPictureKey;
            _altText = newAltText;
        }

        public override bool Equals(System.Object otherPicture)
        {
            if (!(otherPicture is Picture))
            {
                return false;
            }
            else
            {
                Picture newPicture = (Picture) otherPicture;
                bool idIdentity = this.GetId() == newPicture.GetId();
                bool keyIdentity = this.GetPictureKey() == newPicture.GetPictureKey();
                bool altTextIdentity = this.GetAltText() == newPicture.GetAltText();
                return (idIdentity && keyIdentity && altTextIdentity);
            }
        }

        public static List<Picture> GetAll()
        {
            List<Picture> allPictures = new List<Picture>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM pictures;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int pictureId = rdr.GetInt32(0);
                string pictureKey = rdr.GetString(1);
                string pictureAltText = rdr.GetString(2);
                Picture newPicture = new Picture(pictureKey, pictureAltText, pictureId);
                allPictures.Add(newPicture);
            }
            DB.CloseSqlConnection(conn,rdr);
            return allPictures;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand ("INSERT INTO pictures (picture_key, alt_text) OUTPUT INSERTED.id VALUES (@NewKey, @NewAltText);",conn);
            cmd.Parameters.Add(new SqlParameter("@NewKey", this.GetPictureKey()));
            cmd.Parameters.Add(new SqlParameter("@NewAltText", this.GetAltText()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.SetId(rdr.GetInt32(0));
            }

            DB.CloseSqlConnection(conn,rdr);
        }

        public static void DeleteAll()
        {
            DB.DeleteAll("pictures");
        }

        public int GetId()
        {
            return _id;
        }
        public void SetId(int newId)
        {
            _id = newId;
        }
        public string GetPictureKey()
        {
            return _pictureKey;
        }
        public string GetAltText()
        {
            return _altText;
        }
    }
}
