﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CuentaCorrienteExpoGranel.MAXA {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Security/", ConfigurationName="MAXA.movimientos")]
    public interface movimientos {
        
        // CODEGEN: Generating message contract since element name arg0 from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://Security/movimientos/movimientosRequest", ReplyAction="http://Security/movimientos/movimientosResponse")]
        CuentaCorrienteExpoGranel.MAXA.movimientosResponse movimientos(CuentaCorrienteExpoGranel.MAXA.movimientosRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Security/movimientos/movimientosRequest", ReplyAction="http://Security/movimientos/movimientosResponse")]
        System.Threading.Tasks.Task<CuentaCorrienteExpoGranel.MAXA.movimientosResponse> movimientosAsync(CuentaCorrienteExpoGranel.MAXA.movimientosRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class movimientosRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="movimientos", Namespace="http://Security/", Order=0)]
        public CuentaCorrienteExpoGranel.MAXA.movimientosRequestBody Body;
        
        public movimientosRequest() {
        }
        
        public movimientosRequest(CuentaCorrienteExpoGranel.MAXA.movimientosRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class movimientosRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string arg0;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string arg1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string arg2;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string arg3;
        
        public movimientosRequestBody() {
        }
        
        public movimientosRequestBody(string arg0, string arg1, string arg2, string arg3) {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class movimientosResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="movimientosResponse", Namespace="http://Security/", Order=0)]
        public CuentaCorrienteExpoGranel.MAXA.movimientosResponseBody Body;
        
        public movimientosResponse() {
        }
        
        public movimientosResponse(CuentaCorrienteExpoGranel.MAXA.movimientosResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class movimientosResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public movimientosResponseBody() {
        }
        
        public movimientosResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface movimientosChannel : CuentaCorrienteExpoGranel.MAXA.movimientos, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class movimientosClient : System.ServiceModel.ClientBase<CuentaCorrienteExpoGranel.MAXA.movimientos>, CuentaCorrienteExpoGranel.MAXA.movimientos {
        
        public movimientosClient() {
        }
        
        public movimientosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public movimientosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public movimientosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public movimientosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CuentaCorrienteExpoGranel.MAXA.movimientosResponse CuentaCorrienteExpoGranel.MAXA.movimientos.movimientos(CuentaCorrienteExpoGranel.MAXA.movimientosRequest request) {
            return base.Channel.movimientos(request);
        }
        
        public string movimientos(string arg0, string arg1, string arg2, string arg3) {
            CuentaCorrienteExpoGranel.MAXA.movimientosRequest inValue = new CuentaCorrienteExpoGranel.MAXA.movimientosRequest();
            inValue.Body = new CuentaCorrienteExpoGranel.MAXA.movimientosRequestBody();
            inValue.Body.arg0 = arg0;
            inValue.Body.arg1 = arg1;
            inValue.Body.arg2 = arg2;
            inValue.Body.arg3 = arg3;
            CuentaCorrienteExpoGranel.MAXA.movimientosResponse retVal = ((CuentaCorrienteExpoGranel.MAXA.movimientos)(this)).movimientos(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CuentaCorrienteExpoGranel.MAXA.movimientosResponse> CuentaCorrienteExpoGranel.MAXA.movimientos.movimientosAsync(CuentaCorrienteExpoGranel.MAXA.movimientosRequest request) {
            return base.Channel.movimientosAsync(request);
        }
        
        public System.Threading.Tasks.Task<CuentaCorrienteExpoGranel.MAXA.movimientosResponse> movimientosAsync(string arg0, string arg1, string arg2, string arg3) {
            CuentaCorrienteExpoGranel.MAXA.movimientosRequest inValue = new CuentaCorrienteExpoGranel.MAXA.movimientosRequest();
            inValue.Body = new CuentaCorrienteExpoGranel.MAXA.movimientosRequestBody();
            inValue.Body.arg0 = arg0;
            inValue.Body.arg1 = arg1;
            inValue.Body.arg2 = arg2;
            inValue.Body.arg3 = arg3;
            return ((CuentaCorrienteExpoGranel.MAXA.movimientos)(this)).movimientosAsync(inValue);
        }
    }
}
