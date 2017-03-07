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

        public static Picture Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM pictures WHERE id = @PictureId;", conn);
            cmd.Parameters.Add(new SqlParameter("@PictureId", id.ToString()));
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundPictureId = 0;
            string foundPictureKey = null;
            string foundPictureAltText = null;

            while(rdr.Read())
            {
                foundPictureId = rdr.GetInt32(0);
                foundPictureKey = rdr.GetString(1);
                foundPictureAltText = rdr.GetString(2);
            }

            Picture foundPicture = new Picture(foundPictureKey, foundPictureAltText, foundPictureId);
            DB.CloseSqlConnection(conn, rdr);
            return foundPicture;
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

        public void Update(string newKey, string newAltText)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand ("UPDATE pictures SET picture_key = @NewKey, alt_text = @NewAltText WHERE id = @TargetId;",conn);
            cmd.Parameters.Add(new SqlParameter("@TargetId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@NewKey", newKey));
            cmd.Parameters.Add(new SqlParameter("@NewAltText", newAltText));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);

            this.SetPictureKey(newKey);
            this.SetAltText(newAltText);
        }

        public void Delete()
        {
            DB.Delete(this.GetId(), "pictures", "picture", "pictures_products", "reviews_pictures");
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
        public void SetPictureKey(string newPictureKey)
        {
            _pictureKey = newPictureKey;
        }

        public string GetAltText()
        {
            return _altText;
        }
        public void SetAltText(string newAltText)
        {
            _altText = newAltText;
        }
    }
}
