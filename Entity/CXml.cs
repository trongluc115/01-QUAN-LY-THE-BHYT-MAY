using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Entity
{
    public class CXml
    {
        public string ReadXML(int item,string FileName)
        {
            string result="";
            try
            {
                string filename = "..//..//..//xml//"+FileName;
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
             
                result = ds.Tables[0].Rows[0]["P" + item.ToString()].ToString();

            }
            catch
            {
                
            }
            return result;
        }
        public DataSet ReadXML(string FileName)
        {
            DataSet ds = new DataSet();
            try
            {
                string filename = "..//..//..//xml//" + FileName;
             
                ds.ReadXml(filename);

               

            }
            catch
            {

            }
            return ds;
        }
        public void WriteXML(int item,string value, string FileName)
        {
          
            try
            {
                string filename = "..//..//..//xml//"+FileName;
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
                //MessageBox.Show(ds.Tables[0].Rows[0]["P" + item.ToString()].ToString());
                //int so = int.Parse(ds.Tables[0].Rows[0]["P" + item.ToString()].ToString());
                ds.Tables[0].Rows[0]["P" + item.ToString()] = value;
              
              

                ds.WriteXml(filename);

            }
            catch
            {
               
            }
        }
        public bool WriteXML(DataSet ds, string FileName)
        {

            try
            {
                string filename = "..//..//..//xml//" + FileName;
                          
              


                ds.WriteXml(filename);
                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
