﻿using System;

namespace Wingcode.Data.Rest.Model
{


	/// <summary>
	/// The persistent class for the sale_invoice database table.
	/// 
	/// </summary>

	public class SaleInvoice
	{

		public long id { get; set; }

		public decimal balanceAmount { get; set; }

		public DateTime createdAt { get; set; }

		public decimal creditAmount { get; set; }

		public DateTime invoiceDate { get; set; }

		public decimal invoiceDiscount { get; set; }

		public string invoiceNo { get; set; }

		public string invoiceState { get; set; }

		public string invoiceType { get; set; }

		public decimal netAmount { get; set; }

		public decimal paidAmount { get; set; }

		public string payMethod { get; set; }

		public string recordState { get; set; }

		public int saleItem { get; set; }

		public string saleType { get; set; }

		public decimal totalAmount { get; set; }

		public decimal totalCost { get; set; }

		public decimal totalDiscount { get; set; }

		public decimal totalProfit { get; set; }

		public DateTime updatedAt { get; set; }

		// bi-directional many-to-one association to Branch

		public Branch branch { get; set; }

		// bi-directional many-to-one association to Customer

		public Customer customer { get; set; }

		// bi-directional many-to-one association to User

		public User user { get; set; }

	}
}