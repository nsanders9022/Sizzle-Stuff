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
        private int _amount;

        public CartProduct (int newProductId, int newUserId, int newAmount,int newId =0)
        {
            _id = newId;
            _productId = newProductId;
            _userId = newUserId;
            _amount = newAmount;
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
        public int GetAmount()
        {
            return _amount;
        }
    }





}
