using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.ComponentModel;

namespace LibBaocao
{
	public class BaseFormat
	{

		public BaseFormat()
		{
		}
		public bool f_CheckDate(string v_datetime, string v_format)
		{
			try
			{
				string tmp=v_datetime;
				tmp=f_ClearString(tmp).Trim();
				MessageBox.Show(tmp + ":");
				if((tmp.Length>10)&&(tmp.Length<=16))
				{
					try
					{
						System.DateTime ad = new System.DateTime(int.Parse(tmp.Substring(6,4)),int.Parse(tmp.Substring(3,2)),int.Parse(tmp.Substring(0,2)));
						return true;
					}
					catch
					{
						return false;
					}
				}
				else
					if((tmp.Length>16)&&(tmp.Length<=19))//H:m
				{
					try
					{
						System.DateTime ad2 = new System.DateTime(int.Parse(tmp.Substring(6,4)),int.Parse(tmp.Substring(3,2)),int.Parse(tmp.Substring(0,2)),int.Parse(tmp.Substring(11,2)), int.Parse(tmp.Substring(14,2)),0);
						return true;
					}
					catch
					{
						return true;
					}
				}
				else
					if(tmp.Length>19)//H:m
				{
					try
					{
						System.DateTime ad3 = new System.DateTime(int.Parse(tmp.Substring(6,4)),int.Parse(tmp.Substring(3,2)),int.Parse(tmp.Substring(0,2)),int.Parse(tmp.Substring(11,2)), int.Parse(tmp.Substring(14,2)),int.Parse(tmp.Substring(17)));
						return true;
					}
					catch
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		public string f_GetYear(string v_datetime, string v_format)
		{
			try
			{
				System.Globalization.CultureInfo aCul = new System.Globalization.CultureInfo("fr-FR");
				return System.DateTime.ParseExact(v_datetime,v_format,aCul).Year.ToString();
			}
			catch
			{
				return "";
			}
		}
		public string f_TinhNgay(string v_sd, string v_ed, string v_format)
		{
			try
			{
				//System.Windows.Forms.MessageBox.Show(v_sd +"\n"+ v_ed + "\n" +v_format);
				System.Globalization.CultureInfo aCul = new System.Globalization.CultureInfo("fr-FR");
				System.DateTime sd = System.DateTime.ParseExact(v_sd,v_format,aCul);
				System.DateTime ed = System.DateTime.ParseExact(v_ed,v_format,aCul);
				//System.Windows.Forms.MessageBox.Show((ed-sd).ToString());
				string tmp = (ed-sd).ToString();
				return tmp;//.Substring(0,tmp.IndexOf(".")-1);
			}
			catch//(Exception ex)
			{
				//System.Windows.Forms.MessageBox.Show(v_sd +"\n"+ v_ed + "\n" +v_format +"\n"+ ex.ToString());
				return "0";
			}
		}
		public string f_TinhTuoi(string v_sd, string v_ed, string v_nam)
		{
			try
			{
//				System.DateTime asd=System.DateTime.Now;
//				System.DateTime aed=System.DateTime.Now;
//				int tmp=0;
//				if(sd=="")
//				{
//					aed=System.DateTime.Parse(v_ed);
//					tmp = aed.Year - int.Parse(v_nam);
//					if(tmp<=0)
//					{
//						return "001D";
//					}
//					else
//					{
//						return tmp.ToString().PadLeft(3,'0').Substring(0,3) + "T";
//					}
//				}
//				else
//				{
//				}
				return "001D";
			}
			catch
			{
				return "001D";
			}
		}
		public DataSet f_GetFormatString(string v_cul, string v_ngay)
		{
			try
			{
				System.Globalization.CultureInfo aCul = new System.Globalization.CultureInfo(v_cul);
				System.DateTime ad= System.DateTime.Parse(v_ngay);
				DataSet ads = new DataSet("FormatString");
				ads.Tables.Add("FormatString");
				ads.Tables[0].Columns.Add("ma");
				ads.Tables[0].Columns.Add("ten");
				string[] a = aCul.DateTimeFormat.GetAllDateTimePatterns();
				for(int i=0;i<a.Length;i++)
				{
					ads.Tables[0].Rows.Add(new string[] {a[i],a[i].PadRight(30,' ') + " = " + ad.ToString(a[i])});
				}
				return ads;
			}
			catch
			{
				return null;
			}
		}
		public void f_ResizeDG(System.Windows.Forms.DataGrid v_dg)
		{
			try
			{
				int n=v_dg.TableStyles[0].GridColumnStyles.Count;
				int nw=0;
				for(int i=0;i<n;i++)
				{
					nw=nw+v_dg.TableStyles[0].GridColumnStyles[i].Width;
				}
				nw=nw + v_dg.RowHeaderWidth + 4;
				if(v_dg.Width>nw)
				{
					v_dg.TableStyles[0].GridColumnStyles[n-1].Width=v_dg.TableStyles[0].GridColumnStyles[n-1].Width + (v_dg.Width-nw);
				}
				//MessageBox.Show("OK");
			}
			catch
			{
			}
		}
		public void f_ResizeDG(System.Windows.Forms.DataGrid v_dg, int v_index)
		{
			try
			{
				int n=v_dg.TableStyles[0].GridColumnStyles.Count;
				int nw=0;
				for(int i=0;i<n;i++)
				{
					nw=nw+v_dg.TableStyles[0].GridColumnStyles[i].Width;
				}
				nw=nw + v_dg.RowHeaderWidth + 4;
				if(!((v_index>=0)&&(v_index<n)))
				{
					v_index=n-1;
				}
				if(v_dg.Width>nw)
				{
					v_dg.TableStyles[0].GridColumnStyles[v_index].Width=v_dg.TableStyles[0].GridColumnStyles[v_index].Width + (v_dg.Width-nw);
				}
				//MessageBox.Show("OK");
			}
			catch
			{
			}
		}
		public void f_AddBlankLine(System.Data.DataSet ads, int n)
		{
			try
			{
				System.Data.DataRow a = ads.Tables[0].NewRow();
				for(int i=0;i<a.ItemArray.Length;i++)
				{
					a[i]="";
				}
				for(int i=ads.Tables[0].Rows.Count;i<n;i++)
				{
					ads.Tables[0].Rows.Add(a.ItemArray);
				}
			}
			catch
			{
			}
		}
		public int f_GetWidthStringInPixel(System.Windows.Forms.DataGrid v_dg, string v_str)
		{
			try
			{
				int aWidth=0;
				Graphics g = Graphics.FromHwnd(v_dg.Handle); 
				StringFormat sf = new StringFormat(StringFormat.GenericTypographic); 
				SizeF size; 
				size = g.MeasureString(v_str, v_dg.Font, 500, sf); 
				//MessageBox.Show(size.Width.ToString());
				aWidth=(int)(size.Width)+ 30;
				g.Dispose(); 
				return aWidth;
			}
			catch
			{
				return 0;
			}
		}	
		public void f_LoadDG(System.Windows.Forms.DataGrid v_dg, DataSet v_ds, string[] v_CapText)
		{
			v_dg.TableStyles.Clear();
			v_dg.DataSource=null;
			try
			{
				v_dg.RowHeaderWidth=20;
				v_dg.TableStyles.Clear();

				DataGridTableStyle ts = new DataGridTableStyle();
				ts.MappingName = v_ds.Tables[0].TableName.ToString();

				ts.GridLineColor=Color.FromArgb(204,204, 204);
				//ts.ForeColor=m_Color;
				ts.SelectionBackColor=Color.FromArgb(240,240,240);//currentRowBackBrush;
				ts.SelectionForeColor=Color.FromArgb(0,0,255);
				//ts.ForeColor=Color.FromArgb(255,0,0);
				//ts.HeaderBackColor=Color.FromArgb(74,60, 140);
				ts.HeaderBackColor=Color.FromArgb(192,192,192);
				ts.HeaderForeColor=Color.DarkBlue;
				//ts.HeaderForeColor=m_Color;
				//ts.HeaderFont.Bold=true;

				//ts.RowHeaderWidth=20;
				ts.AllowSorting=true;
				v_dg.TableStyles.Add(ts);
				//ts.AlternatingBackColor = Color.Wheat;
				//DataGridTextBoxColumn[] TextCol1=new DataGridTextBoxColumn[ads.Tables[0].Columns.Count];
				for(int i=0;i<v_ds.Tables[0].Columns.Count;i++)
				{
					//TextCol1[i]=new DataGridTextBoxColumn();
					DataGridTextBoxColumn TextCol0=new DataGridTextBoxColumn();
					TextCol0.MappingName = v_ds.Tables[0].Columns[i].ColumnName;
					TextCol0.HeaderText = v_CapText[i];
					TextCol0.Width = f_GetWidthStringInPixel(v_dg, v_CapText[i]);
					TextCol0.ReadOnly = false;
					//TextCol0.SetCellFormat += new FormatCellEventHandler(f_SetCellFormat);
					ts.GridColumnStyles.Add(TextCol0);
					v_dg.TableStyles.Add(ts);
				}

				v_dg.DataSource=v_ds.Tables[0];//adst.Tables[0];
//				CurrencyManager cm= (CurrencyManager)BindingContext[v_dg.DataSource, v_dg.DataMember];
//				DataView dv= (DataView)cm.List;
//				dv.AllowNew=false;
//				dv.AllowDelete=false;
			}
			catch (Exception e)
			{
				MessageBox.Show("mFormat.f_LoadDG()\n" + e.Message);
			}
		}
		public void f_LoadDG(System.Windows.Forms.DataGrid v_dg, DataSet v_ds, string[] v_CapText, string[] v_Fields)
		{
			v_dg.TableStyles.Clear();
			v_dg.DataSource=null;
			try
			{
				//MessageBox.Show(v_CapText.Length.ToString() + "\n" + v_Fields.Length.ToString());
				v_dg.RowHeaderWidth=20;
				v_dg.TableStyles.Clear();

				DataGridTableStyle ts =new DataGridTableStyle();
				ts.MappingName = v_ds.Tables[0].TableName.ToString();

				ts.GridLineColor=Color.FromArgb(204,204, 204);
				//ts.ForeColor=m_Color;
				ts.SelectionBackColor=Color.FromArgb(240,240,240);//currentRowBackBrush;
				ts.SelectionForeColor=Color.FromArgb(0,0,255);
				//ts.HeaderBackColor=Color.FromArgb(74,60, 140);
				ts.HeaderBackColor=Color.FromArgb(223,228,227);
				ts.HeaderForeColor=Color.DarkBlue;
				//ts.HeaderForeColor=m_Color;
				//ts.HeaderFont.Bold=true;

				//ts.RowHeaderWidth=20;
				ts.AllowSorting=true;
				v_dg.TableStyles.Add(ts);
				//ts.AlternatingBackColor = Color.Wheat;
				//DataGridTextBoxColumn[] TextCol1=new DataGridTextBoxColumn[ads.Tables[0].Columns.Count];
				for(int i=0;i<v_Fields.Length;i++)
				{
					DataGridTextBoxColumn TextCol0=new DataGridTextBoxColumn();
					TextCol0.MappingName = v_Fields[i];
					TextCol0.HeaderText = v_CapText[i];
					TextCol0.Width = f_GetWidthStringInPixel(v_dg, v_CapText[i]);
					TextCol0.ReadOnly = false;
					TextCol0.NullText="";
					//TextCol0.SetCellFormat += new FormatCellEventHandler(f_SetCellFormat);
					ts.GridColumnStyles.Add(TextCol0);
					v_dg.TableStyles.Add(ts);
				}

				v_dg.DataSource=v_ds.Tables[0];//adst.Tables[0];
				//				CurrencyManager cm= (CurrencyManager)BindingContext[v_dg.DataSource, v_dg.DataMember];
				//				DataView dv= (DataView)cm.List;
				//				dv.AllowNew=false;
				//				dv.AllowDelete=false;
			}
			catch// (Exception e)
			{
				//MessageBox.Show("mFormat.f_LoadDG()\n" + e.Message);
			}
		}
		public void f_LoadDG(System.Windows.Forms.DataGrid v_dg, DataSet v_ds, string[] v_CapText, string[] v_Fields, string[] v_FieldsBool)
		{
			v_dg.TableStyles.Clear();
			v_dg.DataSource=null;
			try
			{
				//MessageBox.Show(v_CapText.Length.ToString() + "\n" + v_Fields.Length.ToString());
				v_dg.RowHeaderWidth=20;
				v_dg.TableStyles.Clear();

				DataGridTableStyle ts =new DataGridTableStyle();
				ts.MappingName = v_ds.Tables[0].TableName.ToString();

				ts.GridLineColor=Color.FromArgb(204,204, 204);
				//ts.ForeColor=m_Color;
				ts.SelectionBackColor=Color.FromArgb(240,240,240);//currentRowBackBrush;
				ts.SelectionForeColor=Color.FromArgb(0,0,255);
				//ts.HeaderBackColor=Color.FromArgb(74,60, 140);
				ts.HeaderBackColor=Color.FromArgb(223,228,227);
				ts.HeaderForeColor=Color.DarkBlue;
				//ts.HeaderForeColor=m_Color;
				//ts.HeaderFont.Bold=true;

				//ts.RowHeaderWidth=20;
				ts.AllowSorting=true;
				v_dg.TableStyles.Add(ts);
				//ts.AlternatingBackColor = Color.Wheat;
				//DataGridTextBoxColumn[] TextCol1=new DataGridTextBoxColumn[ads.Tables[0].Columns.Count];
				for(int i=0;i<v_Fields.Length;i++)
				{
					if(v_FieldsBool[i]!="1")
					{
						DataGridTextBoxColumn TextCol0=new DataGridTextBoxColumn();
						TextCol0.MappingName = v_Fields[i];
						TextCol0.HeaderText = v_CapText[i];
						TextCol0.Width = f_GetWidthStringInPixel(v_dg, v_CapText[i]);
						TextCol0.ReadOnly = true;
						TextCol0.NullText="";
						//TextCol0.SetCellFormat += new FormatCellEventHandler(f_SetCellFormat);
						ts.GridColumnStyles.Add(TextCol0);
						v_dg.TableStyles.Add(ts);
					}
					else
					{
						//FormattableBooleanColumn TextCol1=new FormattableBooleanColumn();
						DataGridBoolColumn TextCol1=new DataGridBoolColumn();
						TextCol1.MappingName = v_Fields[i];
						TextCol1.HeaderText = v_CapText[i];
						TextCol1.Width = f_GetWidthStringInPixel(v_dg, v_CapText[i]);
						TextCol1.ReadOnly = false;
						TextCol1.AllowNull = false;
						TextCol1.FalseValue="0";
						TextCol1.TrueValue="1";
						TextCol1.Alignment=System.Windows.Forms.HorizontalAlignment.Left;
						//TextCol1.NullText="";
						//TextCol1.SetCellFormat += new FormatCellEventHandler(f_SetCellFormat);
						ts.GridColumnStyles.Add(TextCol1);
						v_dg.TableStyles.Add(ts);
					}
				}

				v_dg.DataSource=v_ds.Tables[0];//adst.Tables[0];
				//				CurrencyManager cm= (CurrencyManager)BindingContext[v_dg.DataSource, v_dg.DataMember];
				//				DataView dv= (DataView)cm.List;
				//				dv.AllowNew=false;
				//				dv.AllowDelete=false;
			}
			catch// (Exception e)
			{
				//MessageBox.Show("mFormat.f_LoadDG()\n" + e.Message);
			}
		}
		public void f_LoadCB(System.Windows.Forms.ComboBox v_cb, System.Data.DataSet v_ds, string v_Display, string v_Value, string v_Select)
		{
			try
			{
				v_cb.DataSource=null;
				v_cb.Items.Clear();
				if(v_ds!=null)
				{
					v_cb.DataSource=v_ds.Tables[0];
					v_cb.DisplayMember=v_Display;
					v_cb.ValueMember=v_Value;
					//v_cb.SelectedValue=v_Select;
					if((v_Select!="")&&(v_Select!=null))
					{
						v_cb.SelectedValue=v_Select;
					}
					else
					{
						v_cb.SelectedIndex=-1;
					}
					//MessageBox.Show("Load Combobox OK <" + v_cb.Name + ">:\n " + v_ds.Tables[0].Rows.Count.ToString());
				}
			}
			catch
			{
				v_cb.DataSource=null;
				//MessageBox.Show("Load Combobox Error <" + v_cb.Name + ">:\n " + ex.Message);
			}
		}
		public void f_ClearAll(System.Windows.Forms.Control vControl)
		{
			try
			{
				string tmp="";
				if(vControl.Controls.Count>0)
				{
					foreach(System.Windows.Forms.Control ac in vControl.Controls)
					{
						try
						{
							//ac.Tag="";
							tmp=ac.GetType().ToString();
							tmp=tmp.Substring(tmp.LastIndexOf(".") + 1);
							if(ac.Controls.Count>0)
							{
								f_ClearAll(ac);
							}
							else
							if(tmp=="TextBox")
							{
								ac.Text="";
							}
							else
								if(tmp=="ComboBox")
							{
								System.Windows.Forms.ComboBox act = (System.Windows.Forms.ComboBox)(ac);
								act.Text="";
								act.SelectedIndex=-1;
							}
							else
								if(tmp=="DateTimePicker")
							{
								System.Windows.Forms.DateTimePicker actt = (System.Windows.Forms.DateTimePicker)(ac);
								//ac1t.Text="";
								bool bc = actt.Checked;
								actt.Value=System.DateTime.Now;
								actt.Checked = bc;
							}
							else
								if(tmp=="CheckBox")
							{
								System.Windows.Forms.CheckBox acttt = (System.Windows.Forms.CheckBox)(ac);
								//ac1t.Text="";
								acttt.Checked=false;
							}
						}
						catch
						{
							//System.Windows.Forms.MessageBox.Show(exx.ToString());
						}
					}
				}
				else
				{
					//vControl.Tag="";
					if(tmp=="TextBox")
					{
						vControl.Text="";
					}
					else
						if(tmp=="ComboBox")
					{
						System.Windows.Forms.ComboBox act_e = (System.Windows.Forms.ComboBox)(vControl);
						act_e.Text="";
						act_e.SelectedIndex=-1;
					}
					else
						if(tmp=="DateTimePicker")
					{
						System.Windows.Forms.DateTimePicker actt_e = (System.Windows.Forms.DateTimePicker)(vControl);
						//ac1t.Text="";
						bool bc = actt_e.Checked;
						actt_e.Value=System.DateTime.Now;
						actt_e.Checked = bc;
					}
					else
						if(tmp=="CheckBox")
					{
						System.Windows.Forms.CheckBox acttt_e = (System.Windows.Forms.CheckBox)(vControl);
						//ac1t.Text="";
						acttt_e.Checked=false;
					}
				}
			}
			catch
			{
				//MessageBox.Show(ex.ToString());
			}
		}
		public void f_EnableAll(System.Windows.Forms.Control vControl, bool vEnabled)
		{
			try
			{
				if(vControl.Controls.Count>0)
				{
					foreach(System.Windows.Forms.Control ac in vControl.Controls)
					{
						if(ac.Controls.Count>0)
						{
							f_EnableAll(ac,vEnabled);
						}
						else
						{
							if(ac.GetType().ToString()!="System.Windows.Forms.Label")
							{
								ac.Enabled=vEnabled;
							}
						}
					}
				}
				else
				{
					vControl.Enabled=vEnabled;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		public void f_VisibaleAll(System.Windows.Forms.Control vControl, bool vVisible)
		{
			try
			{
				if(vControl.Controls.Count>0)
				{
					foreach(System.Windows.Forms.Control ac in vControl.Controls)
					{
						try
						{
							if(ac.Controls.Count>0)
							{
								f_VisibaleAll(ac,vVisible);
							}
							else
							{
								ac.Visible=vVisible;
								//ac.Enabled=vVisible;
							}
						}
						catch(Exception ex1)
						{
							MessageBox.Show(ex1.ToString());
						}
					}
				}
				else
				{
					vControl.Visible=vVisible;
					//vControl.Enabled=vVisible;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		public void f_ValidatedTextBoxNumber(System.Windows.Forms.TextBox v_tb, int v_length)
		{
			if(v_tb.Text!="")
			{
				try
				{
					long tmp = long.Parse(v_tb.Text.Trim());
					if((tmp.ToString().Length<=v_length)&&(tmp>=0))
					{
						v_tb.Text = tmp.ToString().PadLeft(v_length,'0');
					}
					else
					{
						v_tb.Focus();
					}
				}
				catch
				{
					v_tb.Focus();
				}
			}
		}
		public void f_ValidatedTextBoxAndCombobox(System.Windows.Forms.TextBox v_tb, System.Windows.Forms.ComboBox v_cb, int v_length, int v_order)
		{
			//v_order=1:Textbox validate; 
			//v_order=2:Combobox validate; 
			//v_lenght: Số ký tự thêm vào
			//v_char: Ký tự sẽ thêm vào
			try
			{
				if(v_order==1)
				{
					//if(v_tb.Text!="")
					//{
						v_tb.Text=v_tb.Text.Trim().PadLeft(v_length,'0');
						v_cb.SelectedValue=v_tb.Text.Trim();
					//}
				}
				else
				{
					v_tb.Text=v_cb.SelectedValue.ToString().Trim();
				}
			}
			catch
			{
			}
		}
		public void f_CheckBoxInGroup_Validate(System.Windows.Forms.Control v_g, System.Windows.Forms.RadioButton v_r)
		{
			try
			{
				if(v_r.Checked)
				{
					v_g.Tag=v_r.Tag.ToString();
				}
			}
			catch
			{
			}
		}
		public void f_Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				//MessageBox.Show(sender.ToString());
				//MessageBox.Show(e.KeyCode.ToString());
				if(e.KeyCode==Keys.Enter)
				{
					//MessageBox.Show(e.KeyCode.ToString());
					//SendKeys.Send("{Tab}{F4}");
					if(e.Handled==false)
					{
						SendKeys.Send("{Tab}");
						e.Handled=true;
					}
				}
				else
					if((sender.GetType().ToString()=="System.Windows.Forms.ComboBox"))
				{
					//MessageBox.Show(sender.ToString());
					System.Windows.Forms.ComboBox tmp = (System.Windows.Forms.ComboBox)(sender);
					if(tmp.SelectedIndex<0)
					{
						tmp.SelectedIndex=0;
					}
					//SendKeys.Send("{F4}");
				}
			}
			catch
			{
				//MessageBox.Show(ex.ToString());
			}
		}
		public void f_Control_KeyDown_F4(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				//MessageBox.Show(sender.ToString());
				//MessageBox.Show(e.KeyCode.ToString());
				if(e.KeyCode==Keys.Enter)
				{
					//MessageBox.Show(e.KeyCode.ToString());
					if(e.Handled==false)
					{
						SendKeys.Send("{Tab}{F4}");
						e.Handled=false;
					}
					//SendKeys.Send("{Tab}");
				}
				else
					if((sender.GetType().ToString()=="System.Windows.Forms.ComboBox"))
				{
					//MessageBox.Show(sender.ToString());
					System.Windows.Forms.ComboBox tmp = (System.Windows.Forms.ComboBox)(sender);
					////					if(tmp.SelectedIndex<0)
					////					{
					////						tmp.SelectedIndex=0;
					////					}
					//SendKeys.Send("{F4}");
				}
			}
			catch
			{
				//MessageBox.Show(ex.ToString());
			}
		}
		public void f_EventKeyDownAll(System.Windows.Forms.Control v_c)
		{
			try
			{
				if((v_c.Controls.Count>0)&&(v_c.GetType().ToString()!="System.Windows.Forms.DataGrid"))
				{
					foreach(System.Windows.Forms.Control ac in v_c.Controls)
					{
						try
						{
							if(ac.Controls.Count>0)
							{
								f_EventKeyDownAll(ac);
							}
							else
							{
								if((ac.GetType().ToString()!="System.Windows.Forms.Label")&&(ac.GetType().ToString()!="System.Windows.Forms.DataGrid")&&(ac.GetType().ToString()!="System.Windows.Forms.Panel")&&(ac.GetType().ToString()!="System.Windows.Forms.GroupBox"))
								{
									try
									{
										if(v_c.GetNextControl(ac,true).GetType().ToString()=="System.Windows.Forms.ComboBox")
										{
											try
											{
												ac.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown_F4);
											}
											catch
											{
											}
											try
											{
												ac.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
											}
											catch
											{
											}
											ac.KeyDown += new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown_F4);
										}
										else
										{
											try
											{
												ac.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
											}
											catch
											{
											}
											try
											{
												ac.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown_F4);
											}
											catch
											{
											}
											ac.KeyDown += new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
										}
									}
									catch//(Exception exx)
									{
										//MessageBox.Show(v_c.ToString() + "\n" + exx.ToString());
										//ac.KeyDown += new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
									}
								}
							}
						}
						catch//(Exception ex)
						{
							//MessageBox.Show(ex.ToString());
						}
					}
				}
				else
				{
					if((v_c.GetType().ToString()!="System.Windows.Forms.Label")&&(v_c.GetType().ToString()!="System.Windows.Forms.Panel")&&(v_c.GetType().ToString()!="System.Windows.Forms.GroupBox")&&(v_c.GetType().ToString()!="System.Windows.Forms.DataGrid"))
					{
						try
						{
							v_c.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown_F4);
						}
						catch
						{
						}
						try
						{
							v_c.KeyDown -= new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
						}
						catch
						{
						}
						v_c.KeyDown += new System.Windows.Forms.KeyEventHandler(f_Control_KeyDown);
					}
				}
			}
			catch
			{
			}
		}
		public void f_Control_VisibleChanged(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Control tmp = (System.Windows.Forms.Control)(sender);
			tmp.Enabled=tmp.Visible;
		}
		public string f_RemoveNonNumber(string t)
		{
			try
			{
				string t1="";
				string tf="0123456789";
				for(int i=0;i<t.Length;i++)
				{
					if(tf.IndexOf(t.Substring(i,1))>=0)
					{
						t1=t1 + t.Substring(i,1);
					}
				}
				return t1;
			}
			catch
			{
				return t;
			}
		}

		public void f_txtNumberOnly_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(sender.GetType().ToString()=="System.Windows.Forms.TextBox")
				{
					System.Windows.Forms.TextBox ct = (System.Windows.Forms.TextBox)(sender);
					int tmp=ct.SelectionStart;
					int l=ct.Text.Length;
					ct.Text=f_RemoveNonNumber(ct.Text);
					tmp=tmp-(l-ct.Text.Length);
					ct.SelectionStart=tmp;
				}
			}
			catch
			{
			}
		}
		public void f_WriteErr(string v_err)
		{
			try
			{
				System.IO.StreamWriter a= System.IO.File.AppendText("InterfaceErr.txt");
				a.WriteLine("-----------------------------------------------------------");
				a.WriteLine("Ngày: " + System.DateTime.Now.ToString());
				a.WriteLine("");
				a.WriteLine(v_err);
				a.WriteLine("");
				a.Close();
			}
			catch
			{
			}
		}

		public int f_Get_MenuItem(System.Windows.Forms.MenuItem v_it, DataSet v_ds, int v_index, string v_right)
		{
			try
			{
				if((v_it.Text.Trim()!="-")&&(v_it.MenuItems.Count<=0))
				{
					v_ds.Tables[0].Rows.Add(new string[] {v_index.ToString(), v_it.Text.Trim()});
					v_index=v_index + 1;
				}

				if(v_it.MenuItems.Count>0)
				{
					for(int i=0;i<v_it.MenuItems.Count;i++)
					{
						v_index=f_Get_MenuItem(v_it.MenuItems[i], v_ds, v_index, v_right);
					}
				}
				return v_index;
			}
			catch
			{
				return 1;
			}
		}
		public void f_EnabledMenu(System.Windows.Forms.Menu v_menu)
		{
			try
			{
				int aindex=1;
				string aright="";
				DataSet ads= new DataSet();
				ads.Tables.Add("MAINMENU");
				ads.Tables[0].Columns.Add("MA");
				ads.Tables[0].Columns.Add("TEN");
				for(int i=0;i<v_menu.MenuItems.Count;i++)
				{
					aindex = f_Get_MenuItem(v_menu.MenuItems[i],ads,aindex,aright);
				}
			}
			catch
			{
			}
		}
		private int f_CountString(string s, string t)
		{
			try
			{
				string tmp;
				tmp=s.Replace(t,"");
				return (s.Length-tmp.Length)/t.Length;
			}
			catch
			{
				return 0;
			}
		}
		private string f_ClearString(string s)
		{
			try
			{
				string r="";
				string sf="0123456789/: ";
				s=s.Replace("//","/");
				s=s.Replace("  "," ");
				s=s.Replace("::",":");
				for(int i=0;i<s.Length;i++)
				{
					if(sf.IndexOf(s[i].ToString())>=0)
					{
						r=r + s[i].ToString();
					}
				}
				return r;
			}
			catch
			{
				return s;
			}
		}
		public void f_txtDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		public void f_txtDate_TextChanged(object sender, System.EventArgs e)
		{
			System.Windows.Forms.TextBox txtDate = (System.Windows.Forms.TextBox)(sender);
			try
			{
				string tmp=txtDate.Text;
				int p=txtDate.SelectionStart;
				if(p==2)
				{
					if(tmp.Substring(0,p).IndexOf("/")<0)
					{
						if(tmp.Length>p)
						{
							if(tmp[p].ToString()!="/")
							{
								tmp=tmp.Substring(0,p) + "/" + tmp.Substring(p);
							}
							p=p+1;
						}
						else
						{
							tmp=tmp.Substring(0,p) + "/" + tmp.Substring(p);
							p=p+1;
						}
					}
				}
				else
					if(p==5)
				{
					if(tmp.Substring(3,2).IndexOf("/")<0)
					{
						if(tmp.Length>p)
						{
							if(tmp[p].ToString()!="/")
							{
								tmp=tmp.Substring(0,p) + "/" + tmp.Substring(p);
							}
							p=p+1;
						}
						else
						{
							tmp=tmp.Substring(0,p) + "/" + tmp.Substring(p);
							p=p+1;
						}
					}
				}
				else
					if(p==10)
				{
					if(tmp.Substring(6,4).IndexOf(" ")<0)
					{
						if(tmp.Length>p)
						{
							if(tmp[p].ToString()!=" ")
							{
								tmp=tmp.Substring(0,p) + " " + tmp.Substring(p);
							}
							p=p+1;
						}
						else
						{
							tmp=tmp.Substring(0,p) + " " + tmp.Substring(p);
							p=p+1;
						}
					}
				}
				else
					if(p==13)
				{
					if(tmp.Substring(11,2).IndexOf(":")<0)
					{
						if(tmp.Length>p)
						{
							if(tmp[p].ToString()!=":")
							{
								tmp=tmp.Substring(0,p) + ":" + tmp.Substring(p);
							}
							p=p+1;
						}
						else
						{
							tmp=tmp.Substring(0,p) + ":" + tmp.Substring(p);
							p=p+1;
						}
					}
				}
				else
					if(p==16)
				{
					if(tmp.Substring(14,2).IndexOf(":")<0)
					{
						if(tmp.Length>p)
						{
							if(tmp[p].ToString()!=":")
							{
								tmp=tmp.Substring(0,p) + ":" + tmp.Substring(p);
							}
							p=p+1;
						}
						else
						{
							tmp=tmp.Substring(0,p) + ":" + tmp.Substring(p);
							p=p+1;
						}
					}
				}
				else
					if(p>=19)
				{
					tmp=tmp.Substring(0,19);
					p=19;
				}
				txtDate.Text=tmp;
				txtDate.SelectionStart=p;
			}
			catch
			{
				txtDate.Text="";
				txtDate.Tag="";
			}
		}
		public void f_txtDate_Validated(object sender, System.EventArgs e)
		{
			System.Windows.Forms.TextBox txtDate = (System.Windows.Forms.TextBox)(sender);
			txtDate.Text=txtDate.Text.Trim();
			if(txtDate.Text !="")
			{
				try
				{
					string tmp=txtDate.Text.Trim();
					int d=-1,m=-1,y=-1,hh=-1,mm=-1,ss=-1;
					d=int.Parse(tmp.Substring(0,tmp.IndexOf("/")));
					tmp=tmp.Substring(tmp.IndexOf("/") + 1);
					m=int.Parse(tmp.Substring(0,tmp.IndexOf("/")));
					tmp=tmp.Substring(tmp.IndexOf("/") + 1);
					if(tmp.IndexOf(" ")>0)
					{
						try
						{
							y=int.Parse(tmp.Substring(0,tmp.IndexOf(" ")));
							tmp=tmp.Substring(tmp.IndexOf(" ") + 1);

							hh=int.Parse(tmp.Substring(0,tmp.IndexOf(":")));
							tmp=tmp.Substring(tmp.IndexOf(":") + 1);
							mm=int.Parse(tmp.Substring(0,tmp.IndexOf(":")));
							tmp=tmp.Substring(tmp.IndexOf(":") + 1);
							ss=int.Parse(tmp);
						}
						catch
						{
							hh=-1;
						}
					}
					else
					{
						y=int.Parse(tmp);
						tmp="";
					}

					if(y.ToString().Length==1)
					{
						if(y<=int.Parse(System.DateTime.Now.Year.ToString().PadLeft(4,'0').Substring(3)))
						{
							y=int.Parse("200" + y.ToString());
						}
						else
						{
							y=int.Parse("199" + y.ToString());
						}
					}
					else
						if(y.ToString().Length==2)
					{
						y=int.Parse("19" + y.ToString());
					}
					else
						if(y.ToString().Length==3)
					{
						y=int.Parse("1" + y.ToString());
					}

					System.DateTime ad;
					if(hh!=-1)
					{
						ad= new System.DateTime(y,m,d,hh,mm,ss);
						tmp=ad.Day.ToString().PadLeft(2,'0') + "/" + ad.Month.ToString().PadLeft(2,'0') + "/" + ad.Year.ToString() + " " + ad.Hour.ToString().PadLeft(2,'0') + ":" + ad.Minute.ToString().PadLeft(2,'0') + ":" + ad.Second.ToString().PadLeft(2,'0');
					}
					else
					{
						ad= new System.DateTime(y,m,d);
						tmp=ad.Day.ToString().PadLeft(2,'0') + "/" + ad.Month.ToString().PadLeft(2,'0') + "/" + ad.Year.ToString();
					}
					txtDate.Text=tmp;
					txtDate.Tag=tmp;
					txtDate.Update();
				}
				catch
				{
//					if(System.Windows.Forms.MessageBox.Show("Ngày nhập không hợp lệ (ngày/tháng/năm giờ:phút:giây).\n\n- Chọn YES để cập nhật lại ngày.\n- Chọn NO để bỏ qua.","Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Error)==DialogResult.Yes)
//					{
//						txtDate.Focus();
//					}
//					else
//					{
//						txtDate.Text="";
//					}
					txtDate.Text="";
					txtDate.Tag="";
					//txtDate.Update();
				}
			}
		}
		public System.DateTime f_ParseDate(string sd)
		{
			try
			{
				string tmp=sd.Trim();
				int d=-1,m=-1,y=-1,hh=-1,mm=-1,ss=-1;
				d=int.Parse(tmp.Substring(0,tmp.IndexOf("/")));
				tmp=tmp.Substring(tmp.IndexOf("/") + 1);
				m=int.Parse(tmp.Substring(0,tmp.IndexOf("/")));
				tmp=tmp.Substring(tmp.IndexOf("/") + 1);
				if(tmp.IndexOf(" ")>0)
				{
					try
					{
						y=int.Parse(tmp.Substring(0,tmp.IndexOf(" ")));
						tmp=tmp.Substring(tmp.IndexOf(" ") + 1);

						hh=int.Parse(tmp.Substring(0,tmp.IndexOf(":")));
						tmp=tmp.Substring(tmp.IndexOf(":") + 1);
						mm=int.Parse(tmp.Substring(0,tmp.IndexOf(":")));
						tmp=tmp.Substring(tmp.IndexOf(":") + 1);
						ss=int.Parse(tmp);
					}
					catch
					{
						hh=-1;
					}
				}
				else
				{
					y=int.Parse(tmp);
					tmp="";
				}
				if(y.ToString().Length==3)
				{
					y=int.Parse(y.ToString() + "0");
				}
				else
					if(y.ToString().Length==2)
				{
					y=int.Parse("19" + y.ToString());
				}
				else
					if(y.ToString().Length==1)
				{
					y=int.Parse("200" + y.ToString());
				}
				System.DateTime ad;
				if(hh!=-1)
				{
					ad= new System.DateTime(y,m,d,hh,mm,ss);
					tmp=ad.Day.ToString().PadLeft(2,'0') + "/" + ad.Month.ToString().PadLeft(2,'0') + "/" + ad.Year.ToString() + " " + ad.Hour.ToString().PadLeft(2,'0') + ":" + ad.Minute.ToString().PadLeft(2,'0') + ":" + ad.Second.ToString().PadLeft(2,'0');
				}
				else
				{
					ad= new System.DateTime(y,m,d);
					tmp=ad.Day.ToString().PadLeft(2,'0') + "/" + ad.Month.ToString().PadLeft(2,'0') + "/" + ad.Year.ToString();
				}
				return ad;
			}
			catch
			{
				return System.DateTime.Now;
			}
		}
		public string f_GetDate(System.DateTime v_date)
		{
			try
			{
				return (v_date.Day.ToString().PadLeft(2,'0') + "/" +v_date.Month.ToString().PadLeft(2,'0') + "/" + v_date.Year.ToString() + " " + v_date.Hour.ToString().PadLeft(2,'0') + ":" + v_date.Minute.ToString().PadLeft(2,'0') + ":" + v_date.Second.ToString().PadLeft(2,'0'));
			}
			catch
			{
				return "";
			}
		}
		#region NGUYEN
		public string Caps( string s)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(s);
			sb[0] = Char.ToUpper( sb[0]);
			string ret=null;			
			int num = 0;int ispace =0;
			while(num < sb.Length)
			{
				if(Char.IsWhiteSpace(sb[num]))
				{
					//					num++;
					ispace++;
				}
			
				if(!Char.IsWhiteSpace(sb[num])) 							
				{
					if (ispace>0 && num>0)
					{
						sb[num] = Char.ToUpper( sb[num]);
						ispace=0;
					}
				}				
				num++;				
			}
			num = 0;
			ispace=0;
			while(num < sb.Length)
			{
				if(Char.IsWhiteSpace(sb[num]))
				{
					if (ispace<1 && num>0 )
					{
						ret+=sb[num];						
					}
					ispace++;
				}
				else
				{
					ret+=sb[num];
					ispace=0;
				}				
				num++;
				//				ret+=sb[num++];
			}
			return ret;
		}

		public virtual int compareDate(string s, string s1)
		{
			int i1=int.Parse(s.Substring(6,4));
			int k=int.Parse(s.Substring(3,2));
			int i=int.Parse(s.Substring(0,2));
			int j1=int.Parse(s.Substring(6,4));
			int l=int.Parse(s.Substring(3,2));
			int j=int.Parse(s.Substring(0,2));

			if (i1 < j1)
				return - 1;
			if (i1 > j1)
				return 1;
			if (k < l)
				return - 1;
			if (k > l)
				return 1;
			if (i < j)
				return - 1;
			return i <= j?0:1;
		}

		public virtual int compareDateTime(string s, string s1)
		{
			int i1=int.Parse(s.Substring(6,4));
			int k=int.Parse(s.Substring(3,2));
			int i=int.Parse(s.Substring(0,2));
			int k1 = int.Parse(s.Substring(11,2));
			int i2 = int.Parse(s.Substring(14,2));
			int k2 = 0;
			int j1=int.Parse(s1.Substring(6,4));
			int l=int.Parse(s1.Substring(3,2));
			int j=int.Parse(s1.Substring(0,2));
			int l1 = int.Parse(s1.Substring(11,2));
			int j2 = int.Parse(s1.Substring(14,2));
			int l2 = 0;
			if (i1 < j1)
				return - 1;
			if (i1 > j1)
				return 1;
			if (k < l)
				return - 1;
			if (k > l)
				return 1;
			if (i < j)
				return - 1;
			if (i > j)
				return 1;
			if (k1 > l1)
				return 1;
			if (k1 < l1)
				return - 1;
			if (i2 > j2)
				return 1;
			if (i2 < j2)
				return - 1;
			if (k2 > l2)
				return 1;
			return k2 >= l2?0:- 1;
		}

		public bool isValidDateTime(string date)
		{
			return f_CheckDate(date,"dd/mm/yyyy hh:mm");
		}
		public string CurrentDateTime
		{
			get
			{
				return (DateTime.Now.Day.ToString().PadLeft(2,'0'))+"/"+(DateTime.Now.Month.ToString().PadLeft(2,'0'))+"/"+(DateTime.Now.Year.ToString().PadLeft(4,'0'))+" "+(DateTime.Now.Hour.ToString().PadLeft(2,'0'))+":"+(DateTime.Now.Minute.ToString().PadLeft(2,'0'));;
			}
		}
		#endregion

	}
}
