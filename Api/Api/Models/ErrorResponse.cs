﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
