using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LibBaocao;

namespace Report2009
{
	/// <summary>
	/// Summary description for Print.
	/// </summary>
	public class Print
	{
		public Print(){
        }
		public void Printer(DataSet ds,string tenfile,string c1,string c2,int kieu)
		{
			try
			{
                ReportDocument oRpt = new ReportDocument();
                oRpt.Load("..\\..\\..\\report\\" + tenfile, OpenReportMethod.OpenReportByTempCopy);
                oRpt.SetDataSource(ds);
                if (c1 != "")
                {
                    oRpt.DataDefinition.FormulaFields["s_nguoithu"].Text = "'" + c1 + "'";
                    oRpt.DataDefinition.FormulaFields["s_sovaovien"].Text = "'" + c2 + "'";
                }
                if (kieu != 0)
                {
                    oRpt.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;//.PaperA4;
                    oRpt.PrintOptions.PaperOrientation = (kieu == 1) ? PaperOrientation.Portrait : PaperOrientation.Landscape;
                }
                oRpt.PrintToPrinter(1, false, 0, 0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch(Exception ex){MessageBox.Show(ex.Message);}
		}
		public void Printer(AccessData m,DataTable dt,string tenfile,string c1,string c2,string c3,string c4,string c5,string c6,string c7,string c8,string c9,string c10,int kieu)
		{
			try
			{
				ReportDocument oRpt=new ReportDocument();
				oRpt.Load("..\\..\\..\\report\\"+tenfile,OpenReportMethod.OpenReportByTempCopy);
				oRpt.SetDataSource(dt);
				oRpt.DataDefinition.FormulaFields["soyte"].Text="'"+m.Syte+"'";
				oRpt.DataDefinition.FormulaFields["benhvien"].Text="'"+m.Tenbv+"'";
				oRpt.DataDefinition.FormulaFields["c1"].Text="'"+c1+"'";
				oRpt.DataDefinition.FormulaFields["c2"].Text="'"+c2+"'";
				oRpt.DataDefinition.FormulaFields["c3"].Text="'"+c3+"'";
				oRpt.DataDefinition.FormulaFields["c4"].Text="'"+c4+"'";
				oRpt.DataDefinition.FormulaFields["c5"].Text="'"+c5+"'";
				oRpt.DataDefinition.FormulaFields["c6"].Text="'"+c6+"'";
				oRpt.DataDefinition.FormulaFields["c7"].Text="'"+c7+"'";
				oRpt.DataDefinition.FormulaFields["c8"].Text="'"+c8+"'";
				oRpt.DataDefinition.FormulaFields["c9"].Text="'"+c9+"'";
				oRpt.DataDefinition.FormulaFields["c10"].Text="'"+c10+"'";
				oRpt.DataDefinition.FormulaFields["giamdoc"].Text="";
				oRpt.DataDefinition.FormulaFields["phutrach"].Text="";
				oRpt.DataDefinition.FormulaFields["thongke"].Text="";
				oRpt.DataDefinition.FormulaFields["ketoan"].Text="";
				oRpt.DataDefinition.FormulaFields["thukho"].Text="";
				oRpt.PrintOptions.PaperOrientation=(kieu==1)?PaperOrientation.Portrait:PaperOrientation.Landscape;
				oRpt.PrintToPrinter(1,false,0,0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch(Exception ex){MessageBox.Show(ex.Message);}
		}

        public void Printer(AccessData m, DataTable dt, string tenfile, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string c10, int kieu,int banin)
        {
            try
            {
                ReportDocument oRpt = new ReportDocument();
                oRpt.Load("..\\..\\..\\report\\" + tenfile, OpenReportMethod.OpenReportByTempCopy);
                oRpt.SetDataSource(dt);
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + m.Syte + "'";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + m.Tenbv + "'";
                oRpt.DataDefinition.FormulaFields["c1"].Text = "'" + c1 + "'";
                oRpt.DataDefinition.FormulaFields["c2"].Text = "'" + c2 + "'";
                oRpt.DataDefinition.FormulaFields["c3"].Text = "'" + c3 + "'";
                oRpt.DataDefinition.FormulaFields["c4"].Text = "'" + c4 + "'";
                oRpt.DataDefinition.FormulaFields["c5"].Text = "'" + c5 + "'";
                oRpt.DataDefinition.FormulaFields["c6"].Text = "'" + c6 + "'";
                oRpt.DataDefinition.FormulaFields["c7"].Text = "'" + c7 + "'";
                oRpt.DataDefinition.FormulaFields["c8"].Text = "'" + c8 + "'";
                oRpt.DataDefinition.FormulaFields["c9"].Text = "'" + c9 + "'";
                oRpt.DataDefinition.FormulaFields["c10"].Text = "'" + c10 + "'";
                oRpt.DataDefinition.FormulaFields["giamdoc"].Text = "";
                oRpt.DataDefinition.FormulaFields["phutrach"].Text = "";
                oRpt.DataDefinition.FormulaFields["thongke"].Text = "";
                oRpt.DataDefinition.FormulaFields["ketoan"].Text = "";
                oRpt.DataDefinition.FormulaFields["thukho"].Text = "";
                oRpt.PrintOptions.PaperSize = PaperSize.PaperA4;
                oRpt.PrintOptions.PaperOrientation = (kieu == 1) ? PaperOrientation.Portrait : PaperOrientation.Landscape;
                oRpt.PrintToPrinter(banin, false, 0, 0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

		public void Printer(AccessData m,DataSet ds,string tenfile,string c1,string c2,string c3,string c4,string c5,string c6,string c7,string c8,string c9,string c10,int kieu)
		{
			try
			{
				ReportDocument oRpt=new ReportDocument();
				oRpt.Load("..\\..\\..\\report\\"+tenfile,OpenReportMethod.OpenReportByTempCopy);
				oRpt.SetDataSource(ds);
				oRpt.DataDefinition.FormulaFields["soyte"].Text="'"+m.Syte+"'";
				oRpt.DataDefinition.FormulaFields["benhvien"].Text="'"+m.Tenbv+"'";
				oRpt.DataDefinition.FormulaFields["c1"].Text="'"+c1+"'";
				oRpt.DataDefinition.FormulaFields["c2"].Text="'"+c2+"'";
				oRpt.DataDefinition.FormulaFields["c3"].Text="'"+c3+"'";
				oRpt.DataDefinition.FormulaFields["c4"].Text="'"+c4+"'";
				oRpt.DataDefinition.FormulaFields["c5"].Text="'"+c5+"'";
				oRpt.DataDefinition.FormulaFields["c6"].Text="'"+c6+"'";
				oRpt.DataDefinition.FormulaFields["c7"].Text="'"+c7+"'";
				oRpt.DataDefinition.FormulaFields["c8"].Text="'"+c8+"'";
				oRpt.DataDefinition.FormulaFields["c9"].Text="'"+c9+"'";
				oRpt.DataDefinition.FormulaFields["c10"].Text="'"+c10+"'";
				oRpt.DataDefinition.FormulaFields["giamdoc"].Text="";
				oRpt.DataDefinition.FormulaFields["phutrach"].Text="";
				oRpt.DataDefinition.FormulaFields["thongke"].Text="";
				oRpt.DataDefinition.FormulaFields["ketoan"].Text="";
				oRpt.DataDefinition.FormulaFields["thukho"].Text="";
				oRpt.PrintOptions.PaperOrientation=(kieu==1)?PaperOrientation.Portrait:PaperOrientation.Landscape;
				oRpt.PrintToPrinter(1,false,0,0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch(Exception ex){MessageBox.Show(ex.Message);}
		}

		public void Printer(AccessData m,DataSet ds,string tenfile,string msg,int kieu)
		{
			try
			{
				ReportDocument oRpt=new ReportDocument();
				oRpt.Load("..\\..\\..\\report\\"+tenfile,OpenReportMethod.OpenReportByTempCopy);
				oRpt.SetDataSource(ds);
				oRpt.DataDefinition.FormulaFields["SoYTe"].Text="'"+m.Syte+"'";
				oRpt.DataDefinition.FormulaFields["BenhVien"].Text="'"+m.Tenbv+"'";
				oRpt.DataDefinition.FormulaFields["TenBenhAn"].Text="'"+msg+"'";
				//oRpt.PrintOptions.PaperOrientation=(kieu==1)?PaperOrientation.Portrait:PaperOrientation.Landscape;
				oRpt.PrintToPrinter(1,false,0,0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch(Exception ex){MessageBox.Show(ex.Message);}
		}

        //public void Printer(AccessData d, DataSet ds, string tenfile, string msg, int kieu)
        //{
        //    try
        //    {
        //        ReportDocument oRpt = new ReportDocument();
        //        oRpt.Load("..\\..\\..\\report\\" + tenfile, OpenReportMethod.OpenReportByTempCopy);
        //        oRpt.SetDataSource(ds);
        //        oRpt.DataDefinition.FormulaFields["SoYTe"].Text = "'" + d.Syte + "'";
        //        oRpt.DataDefinition.FormulaFields["BenhVien"].Text = "'" + d.Tenbv + "'";
        //        oRpt.DataDefinition.FormulaFields["TenBenhAn"].Text = "'" + msg + "'";
        //        //oRpt.PrintOptions.PaperOrientation=(kieu==1)?PaperOrientation.Portrait:PaperOrientation.Landscape;
        //        oRpt.PrintToPrinter(1, false, 0, 0);
        //        if (oRpt != null)
        //        {
        //            oRpt.Close();
        //            oRpt.Dispose();
        //        }
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

		public void Printer(AccessData m,DataSet ds,string tenfile,string msg,int kieu,int copy)
		{
			try
			{
				ReportDocument oRpt=new ReportDocument();
				oRpt.Load("..\\..\\..\\report\\"+tenfile,OpenReportMethod.OpenReportByTempCopy);
				oRpt.SetDataSource(ds);
				oRpt.DataDefinition.FormulaFields["SoYTe"].Text="'"+m.Syte+"'";
				oRpt.DataDefinition.FormulaFields["BenhVien"].Text="'"+m.Tenbv+"'";
				oRpt.DataDefinition.FormulaFields["TenBenhAn"].Text="'"+msg+"'";
				oRpt.PrintOptions.PaperOrientation=(kieu==1)?PaperOrientation.Portrait:PaperOrientation.Landscape;
				oRpt.PrintToPrinter(copy,false,0,0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		public void Bia(AccessData m,string tenfile,string c1,string c2,string c3,string c4,string c5,string c6)
		{
			try
			{
				ReportDocument oRpt=new ReportDocument();
				oRpt.Load("..\\..\\..\\report\\"+tenfile,OpenReportMethod.OpenReportByTempCopy);
                DataSet ds = new DataSet();
				oRpt.SetDataSource(ds);
				oRpt.DataDefinition.FormulaFields["c1"].Text="'"+c1+"'";
				oRpt.DataDefinition.FormulaFields["c2"].Text="'"+c2+"'";
				oRpt.DataDefinition.FormulaFields["c3"].Text="'"+c3+"'";
				oRpt.DataDefinition.FormulaFields["c4"].Text="'"+c4+"'";
				oRpt.DataDefinition.FormulaFields["c5"].Text="'"+c5+"'";
				oRpt.DataDefinition.FormulaFields["c6"].Text="'"+c6+"'";
				oRpt.DataDefinition.FormulaFields["Giamdoc"].Text="";
				oRpt.PrintOptions.PaperOrientation=PaperOrientation.Landscape;
				oRpt.PrintToPrinter(1,false,0,0);
                if (oRpt != null)
                {
                    oRpt.Close();
                    oRpt.Dispose();
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show (ex.Message);				
			}
		}
	}
}
