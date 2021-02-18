using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteDapperSample.Database
{
    public class SqliteBuilderTest
    {
        //[Fact]
        //[UseReporter(typeof(VisualStudioReporter))]
        //public void Test()
        //{
        //    var sql = "SELECT * FROM Products p";

        //    var builder = new SqliteBuilder(sql)
        //        .Where("p.Name = @name")
        //        .OrderBy("p.Name")
        //        .OrderByDescending("p.CreatedDate")
        //        .ThenByDescending("p.ModifiedDate")
        //        .ThenBy("p.ActivityDate")
        //        .Take(50)
        //        .Skip(1);

        //    var actual = builder.Build();

        //    Approvals.Verify(actual);
        //}
    }

    public class SqliteBuilder
    {
        private string _select;
        private string _where;
        private string _orderBy;
        private string _orderByDescending;
        private string _thenByDescending;
        private string _thenBy;
        private string _skip;
        private string _take;

        private const string OrderBySq = "ORDER BY";
        private const string Desc = "DESC";
        private const string Asc = "ASC";

        public SqliteBuilder(string select)
        {
            _select = select ?? throw new ArgumentNullException(nameof(select));
        }

        public SqliteBuilder Where(string where)
        {
            if (string.IsNullOrWhiteSpace(where)) throw new ArgumentNullException(nameof(where));

            _where = $"WHERE {where}";
            return this;
        }

        public SqliteBuilder OrderBy(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            _orderBy = $"{propertyName} {Asc}";
            return this;
        }

        public SqliteBuilder OrderByDescending(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            _orderByDescending = $"{propertyName} {Desc}";
            return this;
        }

        public SqliteBuilder ThenByDescending(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            _thenByDescending = $"{propertyName} {Desc}";
            return this;
        }

        public SqliteBuilder ThenBy(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            _thenBy = $"{propertyName} {Asc}";
            return this;
        }

        public SqliteBuilder Skip(int n)
        {
            if (n < 0) n = 0;

            _skip = $"offset {n}";
            return this;
        }

        public SqliteBuilder Take(int n)
        {
            if (n < 0) n = 1;

            _take = $"limit {n}";
            return this;
        }

        public string Build()
        {
            var sb = new StringBuilder();

            // Select
            sb.AppendLine(_select);

            // Where
            sb.AppendLine(_where);

            // Order
            if (string.IsNullOrWhiteSpace(_orderBy) == false || string.IsNullOrWhiteSpace(_orderByDescending) == false)
            {
                sb.AppendLine(OrderBySq);

                if (string.IsNullOrWhiteSpace(_orderBy) == false)
                {
                    sb.AppendLine(_orderBy);
                    sb.Append(",");
                }

                if (string.IsNullOrWhiteSpace(_orderByDescending) == false)
                {
                    sb.AppendLine(_orderByDescending);
                    sb.Append(",");
                }

                if (string.IsNullOrWhiteSpace(_thenByDescending) == false)
                {
                    sb.AppendLine(_thenByDescending);
                    sb.Append(",");
                }

                if (string.IsNullOrWhiteSpace(_thenBy) == false)
                {
                    sb.AppendLine(_thenBy);
                    sb.Append(",");
                }

                // Remove last comma
                sb.Remove(sb.Length - 1, 1);
            }

            // Pagging
            sb.AppendLine(_take);
            sb.AppendLine(_skip);
            sb.Append(";");

            return sb.ToString().Trim();
        }
    }
}
