using System;
using EFCoreSample.Core;

namespace EFCoreSample
{
	class Program
	{
		static void Main(string[] args)
		{
			//判断数据库是否存在,如果不存在则创建表
			WechatContext context = new WechatContext();

			if (!context.Database.EnsureCreated())
			{
				Console.WriteLine("Error: Unable to create the mysql database！");
			}
			else
			{
				Console.WriteLine("Create and Connect to MySQL success！");

				//初始化微信用户关系数据
				if (UserHelper.GetWechatUserToDB())
				{
					Console.WriteLine("Init wechat users to MySQL db success！");
				}
				else
				{
					Console.WriteLine("Error: Unable to Init wechat users to MySQL db！");
				}
			}
		}
	}
}