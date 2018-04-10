﻿using ICanPay.Core;
using ICanPay.Core.Response;
using ICanPay.Wechatpay;
using ICanPay.Wechatpay.Domain;
using ICanPay.Wechatpay.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ICanPay.Demo.Controllers
{
    public class WechatpayController : Controller
    {
        private readonly BaseGateway _baseGateway;

        public WechatpayController(IGateways gateways)
        {
            _baseGateway = gateways.Get<WechatpayGateway>();
        }

        [HttpPost]
        public IActionResult PublicPay(string out_trade_no, int total_amount, string body, string open_id)
        {
            var request = new PublicPayRequest();
            request.AddGatewayData(new PublicPayModel()
            {
                Body = body,
                OutTradeNo = out_trade_no,
                TotalAmount = total_amount,
                OpenId = open_id
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult AppPay(string out_trade_no, int total_amount, string body)
        {
            var request = new AppPayRequest();
            request.AddGatewayData(new AppPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult AppletPay(string out_trade_no, int total_amount, string body, string open_id)
        {
            var request = new AppletPayRequest();
            request.AddGatewayData(new AppletPayModel()
            {
                Body = body,
                OutTradeNo = out_trade_no,
                TotalAmount = total_amount,
                OpenId = open_id
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult WapPay(string out_trade_no, int total_amount, string body, string scene_info)
        {
            var request = new WapPayRequest();
            request.AddGatewayData(new WapPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                SceneInfo = scene_info
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult ScanPay(string out_trade_no, string body, int total_amount)
        {
            var request = new ScanPayRequest();
            request.AddGatewayData(new ScanPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);

            return Json(response);
        }

        [HttpPost]
        public IActionResult BarcodePay(string out_trade_no, string auth_code, int total_amount, string body)
        {
            var request = new BarcodePayRequest();
            request.AddGatewayData(new BarcodePayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                AuthCode = auth_code
            });
            request.PaySucceed += BarcodePay_PaySucceed;
            request.PayFailed += BarcodePay_PayFaild;

            var response = _baseGateway.Execute(request);

            return Json(response);
        }

        /// <summary>
        /// 支付成功事件
        /// </summary>
        /// <param name="response">返回结果</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PaySucceed(IResponse response, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 支付失败事件
        /// </summary>
        /// <param name="response">返回结果,可能是BarcodePayResponse/QueryResponse</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PayFaild(IResponse response, string message)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Query(string out_trade_no, string trade_no)
        {
            var request = new QueryRequest();
            request.AddGatewayData(new QueryModel()
            {
                TradeNo = trade_no,
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult Refund(string out_trade_no, string trade_no, int total_amount, int refund_amount, string refund_desc, string out_refund_no)
        {
            var request = new RefundRequest();
            request.AddGatewayData(new RefundModel()
            {
                TradeNo = trade_no,
                RefundAmount = refund_amount,
                RefundDesc = refund_desc,
                OutRefundNo = out_refund_no,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult RefundQuery(string out_trade_no, string trade_no, string out_request_no)
        {
            var request = new RefundQueryRequest();
            request.AddGatewayData(new RefundQueryModel()
            {
                TradeNo = trade_no,
                OutTradeNo = out_trade_no,
                OutRefundNo = out_request_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult Close(string out_trade_no)
        {
            var request = new CloseRequest();
            request.AddGatewayData(new CloseModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult Cancel(string out_trade_no)
        {
            var request = new CancelRequest();
            request.AddGatewayData(new CancelModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult Transfer(string out_trade_no, string openid,string check_name, string true_name, int amount, string desc)
        {
            var request = new TransferRequest();
            request.AddGatewayData(new TransferModel()
            {
                OutTradeNo = out_trade_no,
                OpenId = openid,
                Amount = amount,
                Desc = desc,
                CheckName = check_name,
                TrueName = true_name
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult TransferQuery(string out_trade_no)
        {
            var request = new TransferQueryRequest();
            request.AddGatewayData(new TransferQueryModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult TransferToBank(string out_trade_no, string bank_no, string true_name, string bank_code, int amount, string desc)
        {
            var request = new TransferToBankRequest();
            request.AddGatewayData(new TransferToBankModel()
            {
                OutTradeNo = out_trade_no,
                BankNo = bank_no,
                Amount = amount,
                Desc = desc,
                BankCode = bank_code,
                TrueName = true_name
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public IActionResult TransferToBankQuery(string out_trade_no)
        {
            var request = new TransferToBankQueryRequest();
            request.AddGatewayData(new TransferToBankQueryModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _baseGateway.Execute(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> BillDownload(string bill_date, string bill_type)
        {
            var request = new BillDownloadRequest();
            request.AddGatewayData(new BillDownloadModel()
            {
                BillDate = bill_date,
                BillType = bill_type
            });

            var response = _baseGateway.Execute(request);
            return File(await response.GetBillFileAsync(), "application/gzip");
        }

        [HttpPost]
        public async Task<IActionResult> FundFlowDownload(string bill_date, string account_type)
        {
            var request = new FundFlowDownloadRequest();
            request.AddGatewayData(new FundFlowDownloadModel()
            {
                BillDate = bill_date,
                AccountType = account_type
            });

            var response = _baseGateway.Execute(request);
            return File(await response.GetBillFileAsync(), "application/gzip");
        }
    }
}