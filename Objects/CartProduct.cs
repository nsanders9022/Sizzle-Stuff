using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace OnlineStore.Objects
{
    public class CartProduct
    {
        private int _id;
        private int _productId;
        private int _userId;
        private int _quantity;

        public CartProduct (int newProductId, int newUserId, int newQuantity,int newId =0)
        {
            _id = newId;
            _productId = newProductId;
            _userId = newUserId;
            _quantity = newQuantity;
        }

        public int GetId()
        {
            return _id;
        }
        public int GetProductId()
        {
            return _productId;
        }
        public int GetUserId()
        {
            return _userId;
        }
        public int GetQuantity()
        {
            return _quantity;
        }

        public override bool Equals(System.Object otherCartProduct)
        {
            if (!(otherCartProduct is CartProduct))
            {
                return false;
            }
            else
            {
                CartProduct newCartProduct = (CartProduct) otherCartProduct;
                bool idEquality = this.GetId() == newCartProduct.GetId();
                bool userIdEquality = this.GetUserId() == newCartProduct.GetUserId();
                bool productIdEquality = this.GetProductId() == newCartProduct.GetProductId();
                bool quantityEquality = this.GetQuantity() == newCartProduct.GetQuantity();
                return(idEquality && userIdEquality && productIdEquality && quantityEquality);
            }
        }

        //deletes all rows from cart_product table
        public static void DeleteAll()
        {
            DB.DeleteAll("cart_products");
        }

        //Gets all rows in cartproducts
        public static List<CartProduct> GetAll()
        {

            List<CartProduct> allCartProducts = new List<CartProduct>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cart_products;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int cartProductId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                int productId = rdr.GetInt32(2);
                int quantity = rdr.GetInt32(3);
                CartProduct newCartProduct = new CartProduct(userId, productId, quantity, cartProductId);
                allCartProducts.Add(newCartProduct);
            }

            DB.CloseSqlConnection(conn,rdr);
            return allCartProducts;
        }
    }
}
