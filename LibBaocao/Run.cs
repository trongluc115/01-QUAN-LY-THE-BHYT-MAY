using System;
using System.IO;
using System.Diagnostics;

namespace LibBaocao
{
	public class run
	{
		private string fileName = string.Empty;
		private string arguments = string.Empty;
		private bool waitUntilFinished = false;
		private int waitMillisecs = -1;

		public run(string fileName)
		{
			this.fileName = fileName;
		}
        public run(string fileName, string arguments, bool waitUntilFinished)
        {
            this.fileName = fileName;
            this.arguments = arguments;
            this.waitUntilFinished = waitUntilFinished;
        }
		public run(string fileName, string arguments, int waitMillisecs)
		{
			this.fileName = fileName;
			this.arguments = arguments;
			this.waitMillisecs = waitMillisecs;
		}
		public void Launch()
		{
			using(Process p = new Process())
			{
				p.StartInfo.FileName = this.fileName;
				if(this.arguments.Length > 0)
				{
					p.StartInfo.Arguments = this.arguments;
				}
				//p.StartInfo.WindowStyle=System.Diagnostics.ProcessWindowStyle.Hidden;
				p.Start();
				if(this.waitMillisecs!=-1)
				{
					p.WaitForExit(waitMillisecs);
				}
				else
				{
					if(this.waitUntilFinished==true)
					{
						p.WaitForExit();
					}
				}
				p.Close();
			}
		}
	}
}