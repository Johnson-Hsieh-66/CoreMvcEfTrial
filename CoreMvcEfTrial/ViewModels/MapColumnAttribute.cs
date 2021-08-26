using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcEfTrial.ViewModels
{
    public class MapColumnAttribute : Attribute
    {
        private string _strColumnName;
        private MapKeyType _oKeyType;

        public MapColumnAttribute(string strColumn, MapKeyType oKey = MapKeyType.Genral)
        {
            _strColumnName = strColumn;
            _oKeyType = oKey;
        }
        public MapColumnAttribute(MapKeyType oKey)
        {
            _oKeyType = oKey;
        }
        //public MapColumnAttribute(string column)
        //{
        //    _strColumnName = column;
        //}
        public string ColumnName
        {
            get
            {
                return _strColumnName;
            }
            protected set
            {
                _strColumnName = value;
            }
        }
        public MapKeyType TypeKey
        {
            get
            {
                return _oKeyType;
            }
            protected set
            {
                _oKeyType = value;
            }
        }
    }
    public enum MapKeyType
    {
        System,
        Genral,
        Action
    }
}
