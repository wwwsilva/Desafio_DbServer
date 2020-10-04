using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_DBServer.Model
{
    public class BaseListModel<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }
    }
}
