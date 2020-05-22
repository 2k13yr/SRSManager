using System.Collections.Generic;
using System.Net;
using Common;
using Microsoft.AspNetCore.Mvc;
using SRSApis.SRSManager;
using SRSApis.SRSManager.Apis;
using SRSWebApi.Attributes;
using SRSWebApi.ResponseModules;

namespace SRSWebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class System : ControllerBase
    {
        /// <summary>
        /// ��дonvif�����ļ�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthVerify]
        [Route("/System/ReWriteOnvifConfig")]
        public JsonResult ReWriteOnvifConfig()
        {
            ResponseStruct rs;
            var rt = SystemApis.ReWriteOnvifConfig();
            if (rt)
            {
                rs = new ResponseStruct()
                {
                    Code = ErrorNumber.None,
                    Message = ErrorMessage.ErrorDic![ErrorNumber.None],
                };
            }
            else
            {
                rs = new ResponseStruct()
                {
                    Code = ErrorNumber.Other,
                    Message = ErrorMessage.ErrorDic![ErrorNumber.Other],
                };
            }
            return Program.common.DelApisResult(rt, rs);
        }

        /// <summary>
        /// ���¼���onvif�����ļ�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthVerify]
        [Route("/System/ReloadOnvifConfig")]
        public JsonResult ReloadOnvifConfig()
        {
            ResponseStruct rs;
            var rt = SystemApis.ReloadOnvifConfig();
            if (rt)
            {
                rs = new ResponseStruct()
                {
                    Code = ErrorNumber.None,
                    Message = ErrorMessage.ErrorDic![ErrorNumber.None],
                };
            }
            else
            {
                rs = new ResponseStruct()
                {
                    Code = ErrorNumber.Other,
                    Message = ErrorMessage.ErrorDic![ErrorNumber.Other],
                };
            }
            return Program.common.DelApisResult(rt, rs);
        }

        /// <summary>
        /// ��ȡonvif����ͷʵ�������б�(ip��ַ)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthVerify]
        [Route("/System/GetOnvifMonitorList")]
        public JsonResult GetOnvifMonitorList()
        {
            var rt = OnvifMonitorApis.GetOnvifMonitorsIpAddress(out ResponseStruct rs);
            return Program.common.DelApisResult(rt, rs);
        }

        /// <summary>
        /// ��ȡϵͳ��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthVerify]
        [Route("/System/GetSystemInfo")]
        public JsonResult GetSystemInfo()
        {
            var result = new JsonResult(SystemApis.GetSystemInfo());
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }
        /// <summary>
        /// ��ȡSRSʵ���б�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthVerify]
        [Route("/System/GetSrsInstanceList")]
        public JsonResult GetSrsInstanceList()
        {
            List<string> devs = SystemApis.GetAllSrsManagerDeviceId();
            List<SrsInstanceModule> simlist = new List<SrsInstanceModule>();
            foreach (var dev in devs)
            {
                SrsManager srs = SystemApis.GetSrsManagerInstanceByDeviceId(dev);
                if (srs != null)
                {
                    SrsInstanceModule sim = new SrsInstanceModule()
                    {
                        ConfigPath = srs.srs_ConfigPath,
                        DeviceId = srs.srs_deviceId,
                        IsInit = srs.Is_Init,
                        IsRunning = srs.IsRunning,
                        PidValue = srs.SrsPidValue,
                        SrsInstanceWorkPath = srs.SrsWorkPath,
                        SrsProcessWorkPath = srs.SrsWorkPath + "srs",
                    };
                    simlist.Add(sim);
                }
            }
            var result = new JsonResult(simlist);
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }
    }

}