using System;
using System.Collections;
using System.Collections.Generic;

namespace Library
{
    public class ServiceResponse<T> 
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

       
    }
}
