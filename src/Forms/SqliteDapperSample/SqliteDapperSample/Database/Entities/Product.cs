using System;
using System.Collections.Generic;
using System.Text;


namespace SqliteDapperSample.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return base.ToString();
            }
            return Name;
        }
    }
}
