
using System.Data;



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace = "http://core.intertech.com.tr/", ConfigurationName = "ColleteralSoap")]
public interface ColleteralSoap
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsertRepurchasePaymentAndExpertise", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    void InsertRepurchasePaymentAndExpertise(long WfInstanceId, int CautionBranchCode, int CautionRefNum, string ChannelCode, string TranCode, string UserCode, System.Data.DataSet Entity);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsertRepurchasePaymentAndExpertise", ReplyAction = "*")]
    System.Threading.Tasks.Task InsertRepurchasePaymentAndExpertiseAsync(long WfInstanceId, int CautionBranchCode, int CautionRefNum, string ChannelCode, string TranCode, string UserCode, System.Data.DataSet Entity);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuaranteeLetterWebService", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    GuaranteeReturnMessage[] GetGuaranteeLetterWebService(short CreditAccountBranchCode, int CreditAccountNumber, int CreditAccountSuffix);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuaranteeLetterWebService", ReplyAction = "*")]
    System.Threading.Tasks.Task<GuaranteeReturnMessage[]> GetGuaranteeLetterWebServiceAsync(short CreditAccountBranchCode, int CreditAccountNumber, int CreditAccountSuffix);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    ColleteralWebResultEntity[] GetCautionInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<ColleteralWebResultEntity[]> GetCautionInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGroupCautionReferenceList", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    GroupCollateralWebResultEntity[] GetGroupCautionReferenceList(int BranchCode, int GroupCode, long CustomerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGroupCautionReferenceList", ReplyAction = "*")]
    System.Threading.Tasks.Task<GroupCollateralWebResultEntity[]> GetGroupCautionReferenceListAsync(int BranchCode, int GroupCode, long CustomerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementInfoForRota", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementWebResultEntity[] GetEngagementInfoForRota(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementInfoForRota", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementWebResultEntity[]> GetEngagementInfoForRotaAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementActiveOrPassive", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementEntity GetEngagementActiveOrPassive(int customerNumber, int engagementRef);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementActiveOrPassive", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementEntity> GetEngagementActiveOrPassiveAsync(int customerNumber, int engagementRef);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementWebResultEntity[] GetEngagementInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementWebResultEntity[]> GetEngagementInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementGuarantorExceedingInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementGuarantorInfo[] GetEngagementGuarantorExceedingInfo(int BranchCode, int CustomerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetEngagementGuarantorExceedingInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementGuarantorInfo[]> GetEngagementGuarantorExceedingInfoAsync(int BranchCode, int CustomerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuarantorInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    System.Data.DataSet GetGuarantorInfo(int QueryKey, string QueryType, string ChannelCode, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuarantorInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<System.Data.DataSet> GetGuarantorInfoAsync(int QueryKey, string QueryType, string ChannelCode, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/CheckCustomerAndGurantorCreditsForRetailCreditWF", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    string[] CheckCustomerAndGurantorCreditsForRetailCreditWF(long customerNo, long[] workflowGuarantorCustomers);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/CheckCustomerAndGurantorCreditsForRetailCreditWF", ReplyAction = "*")]
    System.Threading.Tasks.Task<string[]> CheckCustomerAndGurantorCreditsForRetailCreditWFAsync(long customerNo, long[] workflowGuarantorCustomers);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsDepositAccountEODList", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void GetCautionsDepositAccountEODList();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsDepositAccountEODList", ReplyAction = "*")]
    System.Threading.Tasks.Task GetCautionsDepositAccountEODListAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/UpdateCaution_NA_AT", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void UpdateCaution_NA_AT();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/UpdateCaution_NA_AT", ReplyAction = "*")]
    System.Threading.Tasks.Task UpdateCaution_NA_ATAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsHSEODList", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void GetCautionsHSEODList();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsHSEODList", ReplyAction = "*")]
    System.Threading.Tasks.Task GetCautionsHSEODListAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsNOVAEODList", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void GetCautionsNOVAEODList();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetCautionsNOVAEODList", ReplyAction = "*")]
    System.Threading.Tasks.Task GetCautionsNOVAEODListAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetLetterCommisionList", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void GetLetterCommisionList();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetLetterCommisionList", ReplyAction = "*")]
    System.Threading.Tasks.Task GetLetterCommisionListAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagement", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementResult DoAutomaticEngagement(int accountBranchCode, int accountNumber, int accountSuffix, System.DateTime engagementDate, string engagementType, string userCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagement", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementAsync(int accountBranchCode, int accountNumber, int accountSuffix, System.DateTime engagementDate, string engagementType, string userCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementWithGuarantor", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementResult DoAutomaticEngagementWithGuarantor(EngagementInfo engagement);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementWithGuarantor", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementWithGuarantorAsync(EngagementInfo engagement);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementPlain", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementResult DoAutomaticEngagementPlain(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementPlain", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementPlainAsync(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementPlainWithDocumentNo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementResult DoAutomaticEngagementPlainWithDocumentNo(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode, string engagementVersionNo, int mainEngagementNum);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/DoAutomaticEngagementPlainWithDocumentNo", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementPlainWithDocumentNoAsync(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode, string engagementVersionNo, int mainEngagementNum);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetExpertiseInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    ExpertiseReturnMessage[] GetExpertiseInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetExpertiseInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<ExpertiseReturnMessage[]> GetExpertiseInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetTavke", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementEntityTveke GetTavke(long guarantorCustomer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetTavke", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementEntityTveke> GetTavkeAsync(long guarantorCustomer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetTalke", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    EngagementEntityTalke GetTalke(long guarantorCustomer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetTalke", ReplyAction = "*")]
    System.Threading.Tasks.Task<EngagementEntityTalke> GetTalkeAsync(long guarantorCustomer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/CreateLifeColleteral", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    System.Data.DataTable CreateLifeColleteral(
                int branchCode,
                string arrangedInsuranceHand,
                decimal cautionAmount,
                string cautionCurrencyCode,
                string cautionCurrencyName,
                decimal insuranceAmount,
                int insuranceContactNumber,
                string insuranceCurrencyCode,
                System.DateTime insuranceMaturity,
                long policyNumber,
                string policyType,
                int policyZeylNo,
                int creditBranchCode,
                int creditSuffix,
                int customerNumber,
                string insCompany,
                long WFInstanceID,
                string loginName);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/CreateLifeColleteral", ReplyAction = "*")]
    System.Threading.Tasks.Task<System.Data.DataTable> CreateLifeColleteralAsync(
                int branchCode,
                string arrangedInsuranceHand,
                decimal cautionAmount,
                string cautionCurrencyCode,
                string cautionCurrencyName,
                decimal insuranceAmount,
                int insuranceContactNumber,
                string insuranceCurrencyCode,
                System.DateTime insuranceMaturity,
                long policyNumber,
                string policyType,
                int policyZeylNo,
                int creditBranchCode,
                int creditSuffix,
                int customerNumber,
                string insCompany,
                long WFInstanceID,
                string loginName);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoInsert", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    object InsuranceTakenCautionInsuranceInfoInsert(int CustomerNo, int PayerCustomerNo, long PolicyNo, string primAmount, string cautionAmount, int creditRefNumber, int cautionRefNumber, int accountingRefNo, string productCode, System.DateTime policyEndDate, string userName, string bransCode, string isPayer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoInsert", ReplyAction = "*")]
    System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoInsertAsync(int CustomerNo, int PayerCustomerNo, long PolicyNo, string primAmount, string cautionAmount, int creditRefNumber, int cautionRefNumber, int accountingRefNo, string productCode, System.DateTime policyEndDate, string userName, string bransCode, string isPayer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoInsertCekOrTeminat" +
        "", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    object InsuranceTakenCautionInsuranceInfoInsertCekOrTeminat(
                int CustomerNo,
                int PayerCustomerNo,
                long PolicyNo,
                string primAmount,
                string cautionAmount,
                int creditRefNumber,
                int cautionRefNumber,
                int accountingRefNo,
                string productCode,
                System.DateTime policyEndDate,
                string userName,
                string bransCode,
                int branchCode,
                int accountNumber,
                int accountSuffix,
                string genLedgerType,
                string isPayer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoInsertCekOrTeminat" +
        "", ReplyAction = "*")]
    System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoInsertCekOrTeminatAsync(
                int CustomerNo,
                int PayerCustomerNo,
                long PolicyNo,
                string primAmount,
                string cautionAmount,
                int creditRefNumber,
                int cautionRefNumber,
                int accountingRefNo,
                string productCode,
                System.DateTime policyEndDate,
                string userName,
                string bransCode,
                int branchCode,
                int accountNumber,
                int accountSuffix,
                string genLedgerType,
                string isPayer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoCancel", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    object InsuranceTakenCautionInsuranceInfoCancel(long PolicyNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/InsuranceTakenCautionInsuranceInfoCancel", ReplyAction = "*")]
    System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoCancelAsync(long PolicyNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetInsuranceCustomerLoans", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    System.Data.DataTable GetInsuranceCustomerLoans(int customerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetInsuranceCustomerLoans", ReplyAction = "*")]
    System.Threading.Tasks.Task<System.Data.DataTable> GetInsuranceCustomerLoansAsync(int customerNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuaranteeLetterInfo", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    GetGuaranteeLetterInfoResponse[] GetGuaranteeLetterInfo(string CitizenshipNumber, long ExternalClientNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetGuaranteeLetterInfo", ReplyAction = "*")]
    System.Threading.Tasks.Task<GetGuaranteeLetterInfoResponse[]> GetGuaranteeLetterInfoAsync(string CitizenshipNumber, long ExternalClientNo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/IsNewCustomer", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    bool IsNewCustomer(int accountNumber);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/IsNewCustomer", ReplyAction = "*")]
    System.Threading.Tasks.Task<bool> IsNewCustomerAsync(int accountNumber);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetColleteral", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    System.Data.DataSet GetColleteral(int branchCode, int cautionRef, bool withSecurity);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetColleteral", ReplyAction = "*")]
    System.Threading.Tasks.Task<System.Data.DataSet> GetColleteralAsync(int branchCode, int cautionRef, bool withSecurity);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetRepurchaseRecords", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    RepurchasePreviousRecords[] GetRepurchaseRecords(string BranchCode, string RefNum);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetRepurchaseRecords", ReplyAction = "*")]
    System.Threading.Tasks.Task<RepurchasePreviousRecords[]> GetRepurchaseRecordsAsync(string BranchCode, string RefNum);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetRepurchaseSelection", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    RepurchaseSelectedRecords GetRepurchaseSelection(string BranchCode, string RefNum, string IndependentSection, string RepurchaseRecordStatus);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/GetRepurchaseSelection", ReplyAction = "*")]
    System.Threading.Tasks.Task<RepurchaseSelectedRecords> GetRepurchaseSelectionAsync(string BranchCode, string RefNum, string IndependentSection, string RepurchaseRecordStatus);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/RepurchaseWorkflowBatch", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void RepurchaseWorkflowBatch();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/RepurchaseWorkflowBatch", ReplyAction = "*")]
    System.Threading.Tasks.Task RepurchaseWorkflowBatchAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/RepurchaseAccounting", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ForaBaseEntity))]
    void RepurchaseAccounting(RepurchaseAccountingRecord record);

    [System.ServiceModel.OperationContractAttribute(Action = "http://core.intertech.com.tr/RepurchaseAccounting", ReplyAction = "*")]
    System.Threading.Tasks.Task RepurchaseAccountingAsync(RepurchaseAccountingRecord record);
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class GuaranteeReturnMessage
{

    private decimal comRationField;

    private int refBranchCodeField;

    private string refTypeField;

    private int refSeqField;

    private int oldRefBranchCodeField;

    private string oldRefTypeField;

    private int oldRefSeqField;

    private string refNumberField;

    private System.DateTime letterGivenDateField;

    private decimal letterAmountField;

    private string currencyCodeField;

    private System.DateTime letterMaturityField;

    private string continuityFlagField;

    private string draweeField;

    private string letterKindField;

    private string letterTypeField;

    private decimal comAmountField;

    private string expenditureCurrencyCodeField;

    private System.DateTime lastCommissionDateField;

    private string comTahPeriodField;

    private string comTahPeriodRawField;

    private int errorCodeField;

    private string messageField;

    private short expenditureAccountBranchCodeField;

    private int expenditureAccountNumberField;

    private int expenditureAccountSuffixField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public decimal ComRation
    {
        get
        {
            return this.comRationField;
        }
        set
        {
            this.comRationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int RefBranchCode
    {
        get
        {
            return this.refBranchCodeField;
        }
        set
        {
            this.refBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string RefType
    {
        get
        {
            return this.refTypeField;
        }
        set
        {
            this.refTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int RefSeq
    {
        get
        {
            return this.refSeqField;
        }
        set
        {
            this.refSeqField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public int OldRefBranchCode
    {
        get
        {
            return this.oldRefBranchCodeField;
        }
        set
        {
            this.oldRefBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string OldRefType
    {
        get
        {
            return this.oldRefTypeField;
        }
        set
        {
            this.oldRefTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public int OldRefSeq
    {
        get
        {
            return this.oldRefSeqField;
        }
        set
        {
            this.oldRefSeqField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string RefNumber
    {
        get
        {
            return this.refNumberField;
        }
        set
        {
            this.refNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public System.DateTime LetterGivenDate
    {
        get
        {
            return this.letterGivenDateField;
        }
        set
        {
            this.letterGivenDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public decimal LetterAmount
    {
        get
        {
            return this.letterAmountField;
        }
        set
        {
            this.letterAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string CurrencyCode
    {
        get
        {
            return this.currencyCodeField;
        }
        set
        {
            this.currencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public System.DateTime LetterMaturity
    {
        get
        {
            return this.letterMaturityField;
        }
        set
        {
            this.letterMaturityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string ContinuityFlag
    {
        get
        {
            return this.continuityFlagField;
        }
        set
        {
            this.continuityFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string Drawee
    {
        get
        {
            return this.draweeField;
        }
        set
        {
            this.draweeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string LetterKind
    {
        get
        {
            return this.letterKindField;
        }
        set
        {
            this.letterKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public string LetterType
    {
        get
        {
            return this.letterTypeField;
        }
        set
        {
            this.letterTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public decimal ComAmount
    {
        get
        {
            return this.comAmountField;
        }
        set
        {
            this.comAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public string ExpenditureCurrencyCode
    {
        get
        {
            return this.expenditureCurrencyCodeField;
        }
        set
        {
            this.expenditureCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
    public System.DateTime LastCommissionDate
    {
        get
        {
            return this.lastCommissionDateField;
        }
        set
        {
            this.lastCommissionDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
    public string ComTahPeriod
    {
        get
        {
            return this.comTahPeriodField;
        }
        set
        {
            this.comTahPeriodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
    public string ComTahPeriodRaw
    {
        get
        {
            return this.comTahPeriodRawField;
        }
        set
        {
            this.comTahPeriodRawField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
    public int ErrorCode
    {
        get
        {
            return this.errorCodeField;
        }
        set
        {
            this.errorCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
    public string Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
    public short ExpenditureAccountBranchCode
    {
        get
        {
            return this.expenditureAccountBranchCodeField;
        }
        set
        {
            this.expenditureAccountBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
    public int ExpenditureAccountNumber
    {
        get
        {
            return this.expenditureAccountNumberField;
        }
        set
        {
            this.expenditureAccountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
    public int ExpenditureAccountSuffix
    {
        get
        {
            return this.expenditureAccountSuffixField;
        }
        set
        {
            this.expenditureAccountSuffixField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class RepurchaseAccountingRecord
{

    private short tranBranchCodeField;

    private int creditAccountBranchCodeField;

    private int creditAccountNumberField;

    private int creditAccountSuffixField;

    private int debitAccountBranchCodeField;

    private int debitAccountNumberField;

    private int debitAccountSuffixField;

    private decimal amountField;

    private string explanationField;

    private string currencyCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public short TranBranchCode
    {
        get
        {
            return this.tranBranchCodeField;
        }
        set
        {
            this.tranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int CreditAccountBranchCode
    {
        get
        {
            return this.creditAccountBranchCodeField;
        }
        set
        {
            this.creditAccountBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public int CreditAccountNumber
    {
        get
        {
            return this.creditAccountNumberField;
        }
        set
        {
            this.creditAccountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int CreditAccountSuffix
    {
        get
        {
            return this.creditAccountSuffixField;
        }
        set
        {
            this.creditAccountSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public int DebitAccountBranchCode
    {
        get
        {
            return this.debitAccountBranchCodeField;
        }
        set
        {
            this.debitAccountBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public int DebitAccountNumber
    {
        get
        {
            return this.debitAccountNumberField;
        }
        set
        {
            this.debitAccountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public int DebitAccountSuffix
    {
        get
        {
            return this.debitAccountSuffixField;
        }
        set
        {
            this.debitAccountSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public decimal Amount
    {
        get
        {
            return this.amountField;
        }
        set
        {
            this.amountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string Explanation
    {
        get
        {
            return this.explanationField;
        }
        set
        {
            this.explanationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string CurrencyCode
    {
        get
        {
            return this.currencyCodeField;
        }
        set
        {
            this.currencyCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class RepurchaseExpertiseEntity
{

    private long workfowNumberField;

    private decimal repurchaseExpertiseAmountField;

    private decimal repurchaseExpertiseInsuranceAmountField;

    private System.DateTime repurchaseExpertiseDateField;

    private string repurchaseExpertiseCurrencyCodeField;

    private string repurchaseExpertiseInsuranceCurrencyCodeField;

    private string repurchaseExpertiseTCKNField;

    private string repurchaseExpertiseReportNoField;

    private string repurchaseExpertiseReviewedField;

    private System.DateTime repurchaseExpertiseReviewDateField;

    private string repurchaseExpertiseCompanyField;

    private decimal repurchasePaymentAmountField;

    private System.DateTime repurchasePaymentDateField;

    private string repurchasePaymentCurrencyCodeField;

    private string repurchaseExpertiseCompanyNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public long WorkfowNumber
    {
        get
        {
            return this.workfowNumberField;
        }
        set
        {
            this.workfowNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public decimal RepurchaseExpertiseAmount
    {
        get
        {
            return this.repurchaseExpertiseAmountField;
        }
        set
        {
            this.repurchaseExpertiseAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public decimal RepurchaseExpertiseInsuranceAmount
    {
        get
        {
            return this.repurchaseExpertiseInsuranceAmountField;
        }
        set
        {
            this.repurchaseExpertiseInsuranceAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public System.DateTime RepurchaseExpertiseDate
    {
        get
        {
            return this.repurchaseExpertiseDateField;
        }
        set
        {
            this.repurchaseExpertiseDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string RepurchaseExpertiseCurrencyCode
    {
        get
        {
            return this.repurchaseExpertiseCurrencyCodeField;
        }
        set
        {
            this.repurchaseExpertiseCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string RepurchaseExpertiseInsuranceCurrencyCode
    {
        get
        {
            return this.repurchaseExpertiseInsuranceCurrencyCodeField;
        }
        set
        {
            this.repurchaseExpertiseInsuranceCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string RepurchaseExpertiseTCKN
    {
        get
        {
            return this.repurchaseExpertiseTCKNField;
        }
        set
        {
            this.repurchaseExpertiseTCKNField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string RepurchaseExpertiseReportNo
    {
        get
        {
            return this.repurchaseExpertiseReportNoField;
        }
        set
        {
            this.repurchaseExpertiseReportNoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string RepurchaseExpertiseReviewed
    {
        get
        {
            return this.repurchaseExpertiseReviewedField;
        }
        set
        {
            this.repurchaseExpertiseReviewedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public System.DateTime RepurchaseExpertiseReviewDate
    {
        get
        {
            return this.repurchaseExpertiseReviewDateField;
        }
        set
        {
            this.repurchaseExpertiseReviewDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string RepurchaseExpertiseCompany
    {
        get
        {
            return this.repurchaseExpertiseCompanyField;
        }
        set
        {
            this.repurchaseExpertiseCompanyField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public decimal RepurchasePaymentAmount
    {
        get
        {
            return this.repurchasePaymentAmountField;
        }
        set
        {
            this.repurchasePaymentAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public System.DateTime RepurchasePaymentDate
    {
        get
        {
            return this.repurchasePaymentDateField;
        }
        set
        {
            this.repurchasePaymentDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string RepurchasePaymentCurrencyCode
    {
        get
        {
            return this.repurchasePaymentCurrencyCodeField;
        }
        set
        {
            this.repurchasePaymentCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string RepurchaseExpertiseCompanyName
    {
        get
        {
            return this.repurchaseExpertiseCompanyNameField;
        }
        set
        {
            this.repurchaseExpertiseCompanyNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class RepurchaseSelectedRecords
{

    private RepurchaseExpertiseEntity[] repurchaseExpertiseInfoField;

    private RepurchaseExpertiseEntity[] repurchasePaymentInfoField;

    private string repurchaseExpertiseKindField;

    private decimal repurchaseAmountField;

    private string repurchaseCurrencyCodeField;

    private string repurchaseRecordStatusField;

    private string repurchaseStatusReasonField;

    private System.DateTime repurchaseStatusDateField;

    private string repurchaseCurrencyNameField;

    private System.DateTime repurchaseMaturityField;

    private string repurchaseContributionMarginField;

    private string repurchaseExplanationField;

    private string expenseOwnerField;

    private string rentalIncomeField;

    private string rentalIncomeOwnerField;

    private System.DateTime repurchaseDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
    public RepurchaseExpertiseEntity[] RepurchaseExpertiseInfo
    {
        get
        {
            return this.repurchaseExpertiseInfoField;
        }
        set
        {
            this.repurchaseExpertiseInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
    public RepurchaseExpertiseEntity[] RepurchasePaymentInfo
    {
        get
        {
            return this.repurchasePaymentInfoField;
        }
        set
        {
            this.repurchasePaymentInfoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string RepurchaseExpertiseKind
    {
        get
        {
            return this.repurchaseExpertiseKindField;
        }
        set
        {
            this.repurchaseExpertiseKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public decimal RepurchaseAmount
    {
        get
        {
            return this.repurchaseAmountField;
        }
        set
        {
            this.repurchaseAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string RepurchaseCurrencyCode
    {
        get
        {
            return this.repurchaseCurrencyCodeField;
        }
        set
        {
            this.repurchaseCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string RepurchaseRecordStatus
    {
        get
        {
            return this.repurchaseRecordStatusField;
        }
        set
        {
            this.repurchaseRecordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string RepurchaseStatusReason
    {
        get
        {
            return this.repurchaseStatusReasonField;
        }
        set
        {
            this.repurchaseStatusReasonField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public System.DateTime RepurchaseStatusDate
    {
        get
        {
            return this.repurchaseStatusDateField;
        }
        set
        {
            this.repurchaseStatusDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string RepurchaseCurrencyName
    {
        get
        {
            return this.repurchaseCurrencyNameField;
        }
        set
        {
            this.repurchaseCurrencyNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public System.DateTime RepurchaseMaturity
    {
        get
        {
            return this.repurchaseMaturityField;
        }
        set
        {
            this.repurchaseMaturityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string RepurchaseContributionMargin
    {
        get
        {
            return this.repurchaseContributionMarginField;
        }
        set
        {
            this.repurchaseContributionMarginField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public string RepurchaseExplanation
    {
        get
        {
            return this.repurchaseExplanationField;
        }
        set
        {
            this.repurchaseExplanationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string ExpenseOwner
    {
        get
        {
            return this.expenseOwnerField;
        }
        set
        {
            this.expenseOwnerField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string RentalIncome
    {
        get
        {
            return this.rentalIncomeField;
        }
        set
        {
            this.rentalIncomeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string RentalIncomeOwner
    {
        get
        {
            return this.rentalIncomeOwnerField;
        }
        set
        {
            this.rentalIncomeOwnerField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public System.DateTime RepurchaseDate
    {
        get
        {
            return this.repurchaseDateField;
        }
        set
        {
            this.repurchaseDateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class RepurchasePreviousRecords
{

    private string repurchaseExpertiseKindField;

    private string repurchaseSectionField;

    private string repurchaseRecordsStatusField;

    private string repurchaseRefField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string RepurchaseExpertiseKind
    {
        get
        {
            return this.repurchaseExpertiseKindField;
        }
        set
        {
            this.repurchaseExpertiseKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string RepurchaseSection
    {
        get
        {
            return this.repurchaseSectionField;
        }
        set
        {
            this.repurchaseSectionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string RepurchaseRecordsStatus
    {
        get
        {
            return this.repurchaseRecordsStatusField;
        }
        set
        {
            this.repurchaseRecordsStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string RepurchaseRef
    {
        get
        {
            return this.repurchaseRefField;
        }
        set
        {
            this.repurchaseRefField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class GetGuaranteeLetterInfoResponse
{

    private long creditAccountNumberField;

    private short creditAccountBranchCodeField;

    private decimal letterAmountField;

    private string currencyCodeField;

    private string letterTypeField;

    private string letterKindField;

    private string draweeField;

    private System.DateTime letterMaturityField;

    private decimal comRatioField;

    private string recordStatusField;

    private string messageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public long CreditAccountNumber
    {
        get
        {
            return this.creditAccountNumberField;
        }
        set
        {
            this.creditAccountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public short CreditAccountBranchCode
    {
        get
        {
            return this.creditAccountBranchCodeField;
        }
        set
        {
            this.creditAccountBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public decimal LetterAmount
    {
        get
        {
            return this.letterAmountField;
        }
        set
        {
            this.letterAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string CurrencyCode
    {
        get
        {
            return this.currencyCodeField;
        }
        set
        {
            this.currencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string LetterType
    {
        get
        {
            return this.letterTypeField;
        }
        set
        {
            this.letterTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string LetterKind
    {
        get
        {
            return this.letterKindField;
        }
        set
        {
            this.letterKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string Drawee
    {
        get
        {
            return this.draweeField;
        }
        set
        {
            this.draweeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public System.DateTime LetterMaturity
    {
        get
        {
            return this.letterMaturityField;
        }
        set
        {
            this.letterMaturityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public decimal ComRatio
    {
        get
        {
            return this.comRatioField;
        }
        set
        {
            this.comRatioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementEntityTalkeGrid
{

    private int customerNumberField;

    private string customerNameField;

    private int engagementRefField;

    private int tranBranchCodeField;

    private long guarantorCustomerNumberField;

    private string guarantorCustomerNameField;

    private decimal engagementAmountField;

    private string engagementCurrencyCodeField;

    private string engagementTypeField;

    private int mainEngagementNumField;

    private decimal engagementAmountTLField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string CustomerName
    {
        get
        {
            return this.customerNameField;
        }
        set
        {
            this.customerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int TranBranchCode
    {
        get
        {
            return this.tranBranchCodeField;
        }
        set
        {
            this.tranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public long GuarantorCustomerNumber
    {
        get
        {
            return this.guarantorCustomerNumberField;
        }
        set
        {
            this.guarantorCustomerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string GuarantorCustomerName
    {
        get
        {
            return this.guarantorCustomerNameField;
        }
        set
        {
            this.guarantorCustomerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public decimal EngagementAmount
    {
        get
        {
            return this.engagementAmountField;
        }
        set
        {
            this.engagementAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string EngagementCurrencyCode
    {
        get
        {
            return this.engagementCurrencyCodeField;
        }
        set
        {
            this.engagementCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string EngagementType
    {
        get
        {
            return this.engagementTypeField;
        }
        set
        {
            this.engagementTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public int MainEngagementNum
    {
        get
        {
            return this.mainEngagementNumField;
        }
        set
        {
            this.mainEngagementNumField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public decimal EngagementAmountTL
    {
        get
        {
            return this.engagementAmountTLField;
        }
        set
        {
            this.engagementAmountTLField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementEntityTalke
{

    private decimal totalAmountField;

    private EngagementEntityTalkeGrid[] engagementEntityTalkesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public decimal TotalAmount
    {
        get
        {
            return this.totalAmountField;
        }
        set
        {
            this.totalAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
    public EngagementEntityTalkeGrid[] EngagementEntityTalkes
    {
        get
        {
            return this.engagementEntityTalkesField;
        }
        set
        {
            this.engagementEntityTalkesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementEntityTveke
{

    private decimal totalAmountField;

    private EngagementEntityTvekeGrid[] engagementEntityTvekesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public decimal TotalAmount
    {
        get
        {
            return this.totalAmountField;
        }
        set
        {
            this.totalAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
    public EngagementEntityTvekeGrid[] EngagementEntityTvekes
    {
        get
        {
            return this.engagementEntityTvekesField;
        }
        set
        {
            this.engagementEntityTvekesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementEntityTvekeGrid : EngagementEntity
{

    private decimal engagementAmountTLField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public decimal EngagementAmountTL
    {
        get
        {
            return this.engagementAmountTLField;
        }
        set
        {
            this.engagementAmountTLField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EngagementEntityTvekeGrid))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementEntity : ForaBaseEntity
{

    private System.DateTime maturityDateField;

    private string problemCodeField;

    private string archiveCodeField;

    private int customerNumberField;

    private string customerNameField;

    private int engagementRefField;

    private int tranBranchCodeField;

    private System.DateTime engagementDateField;

    private System.DateTime tranDateField;

    private decimal engagementAmountField;

    private string engagementCurrencyCodeField;

    private string taxFlagField;

    private string engagementTypeField;

    private int mainEngagementNumField;

    private string guarantorFlagField;

    private string engagementKindField;

    private string lastUpdatingChannelCodeField;

    private string lastUpdatingTranCodeField;

    private string lastUpdatingUserCodeField;

    private System.DateTime lastUpdateDateField;

    private string recordStatusField;

    private string explanationField;

    private int lineNumberField;

    private EngagementSuffixEntity[] engagementSuffixesField;

    private EngagementGuarantorEntity[] engagementGuarantorsField;

    private EngagementDocumentEntity[] engagementDocumentsField;

    private long workflowActionNumberField;

    private string externalChannelCodeField;

    private int accountingLogNumberField;

    private string kGFGuaranteeKindField;

    private int kGFEngagementNoField;

    private decimal kGFEngagementAmountField;

    private int kGFGuaranteePercentageField;

    private string standartEngagementFormatField;

    private int rbsGivenAccountNumberField;

    private string rbsGivenAccountNameField;

    private string isThereEngagementPeriodField;

    private System.DateTime engagementEndDateField;

    private string engagementVersionNumberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public System.DateTime MaturityDate
    {
        get
        {
            return this.maturityDateField;
        }
        set
        {
            this.maturityDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string ProblemCode
    {
        get
        {
            return this.problemCodeField;
        }
        set
        {
            this.problemCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string ArchiveCode
    {
        get
        {
            return this.archiveCodeField;
        }
        set
        {
            this.archiveCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string CustomerName
    {
        get
        {
            return this.customerNameField;
        }
        set
        {
            this.customerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public int TranBranchCode
    {
        get
        {
            return this.tranBranchCodeField;
        }
        set
        {
            this.tranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public System.DateTime EngagementDate
    {
        get
        {
            return this.engagementDateField;
        }
        set
        {
            this.engagementDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public System.DateTime TranDate
    {
        get
        {
            return this.tranDateField;
        }
        set
        {
            this.tranDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public decimal EngagementAmount
    {
        get
        {
            return this.engagementAmountField;
        }
        set
        {
            this.engagementAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string EngagementCurrencyCode
    {
        get
        {
            return this.engagementCurrencyCodeField;
        }
        set
        {
            this.engagementCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public string TaxFlag
    {
        get
        {
            return this.taxFlagField;
        }
        set
        {
            this.taxFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string EngagementType
    {
        get
        {
            return this.engagementTypeField;
        }
        set
        {
            this.engagementTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public int MainEngagementNum
    {
        get
        {
            return this.mainEngagementNumField;
        }
        set
        {
            this.mainEngagementNumField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string GuarantorFlag
    {
        get
        {
            return this.guarantorFlagField;
        }
        set
        {
            this.guarantorFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public string EngagementKind
    {
        get
        {
            return this.engagementKindField;
        }
        set
        {
            this.engagementKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public string LastUpdatingChannelCode
    {
        get
        {
            return this.lastUpdatingChannelCodeField;
        }
        set
        {
            this.lastUpdatingChannelCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public string LastUpdatingTranCode
    {
        get
        {
            return this.lastUpdatingTranCodeField;
        }
        set
        {
            this.lastUpdatingTranCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
    public string LastUpdatingUserCode
    {
        get
        {
            return this.lastUpdatingUserCodeField;
        }
        set
        {
            this.lastUpdatingUserCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
    public System.DateTime LastUpdateDate
    {
        get
        {
            return this.lastUpdateDateField;
        }
        set
        {
            this.lastUpdateDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
    public string Explanation
    {
        get
        {
            return this.explanationField;
        }
        set
        {
            this.explanationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
    public int LineNumber
    {
        get
        {
            return this.lineNumberField;
        }
        set
        {
            this.lineNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 23)]
    public EngagementSuffixEntity[] EngagementSuffixes
    {
        get
        {
            return this.engagementSuffixesField;
        }
        set
        {
            this.engagementSuffixesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 24)]
    public EngagementGuarantorEntity[] EngagementGuarantors
    {
        get
        {
            return this.engagementGuarantorsField;
        }
        set
        {
            this.engagementGuarantorsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 25)]
    public EngagementDocumentEntity[] EngagementDocuments
    {
        get
        {
            return this.engagementDocumentsField;
        }
        set
        {
            this.engagementDocumentsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
    public long WorkflowActionNumber
    {
        get
        {
            return this.workflowActionNumberField;
        }
        set
        {
            this.workflowActionNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
    public string ExternalChannelCode
    {
        get
        {
            return this.externalChannelCodeField;
        }
        set
        {
            this.externalChannelCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
    public int AccountingLogNumber
    {
        get
        {
            return this.accountingLogNumberField;
        }
        set
        {
            this.accountingLogNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
    public string KGFGuaranteeKind
    {
        get
        {
            return this.kGFGuaranteeKindField;
        }
        set
        {
            this.kGFGuaranteeKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
    public int KGFEngagementNo
    {
        get
        {
            return this.kGFEngagementNoField;
        }
        set
        {
            this.kGFEngagementNoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
    public decimal KGFEngagementAmount
    {
        get
        {
            return this.kGFEngagementAmountField;
        }
        set
        {
            this.kGFEngagementAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
    public int KGFGuaranteePercentage
    {
        get
        {
            return this.kGFGuaranteePercentageField;
        }
        set
        {
            this.kGFGuaranteePercentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 33)]
    public string StandartEngagementFormat
    {
        get
        {
            return this.standartEngagementFormatField;
        }
        set
        {
            this.standartEngagementFormatField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 34)]
    public int RbsGivenAccountNumber
    {
        get
        {
            return this.rbsGivenAccountNumberField;
        }
        set
        {
            this.rbsGivenAccountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 35)]
    public string RbsGivenAccountName
    {
        get
        {
            return this.rbsGivenAccountNameField;
        }
        set
        {
            this.rbsGivenAccountNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 36)]
    public string IsThereEngagementPeriod
    {
        get
        {
            return this.isThereEngagementPeriodField;
        }
        set
        {
            this.isThereEngagementPeriodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 37)]
    public System.DateTime EngagementEndDate
    {
        get
        {
            return this.engagementEndDateField;
        }
        set
        {
            this.engagementEndDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 38)]
    public string EngagementVersionNumber
    {
        get
        {
            return this.engagementVersionNumberField;
        }
        set
        {
            this.engagementVersionNumberField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementSuffixEntity
{

    private int accountSuffixField;

    private decimal amountField;

    private string recordStatusField;

    private string productCodeField;

    private string currencyCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int AccountSuffix
    {
        get
        {
            return this.accountSuffixField;
        }
        set
        {
            this.accountSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public decimal Amount
    {
        get
        {
            return this.amountField;
        }
        set
        {
            this.amountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string ProductCode
    {
        get
        {
            return this.productCodeField;
        }
        set
        {
            this.productCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string CurrencyCode
    {
        get
        {
            return this.currencyCodeField;
        }
        set
        {
            this.currencyCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementGuarantorEntity
{

    private long guarantorCustomerNumberField;

    private string guarantorCustomerNameField;

    private decimal guarantorAmountField;

    private string guarantorCurrencyCodeField;

    private string recordStatusField;

    private System.DateTime guarantorEntranceDateField;

    private System.DateTime guarantorEndDateField;

    private string guarantorExtensionFlagField;

    private System.DateTime guarantorExtensionDateField;

    private string responsibleOldDebtFlagField;

    private string guarantorRucuExistsField;

    private System.DateTime guarantorRucuDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public long GuarantorCustomerNumber
    {
        get
        {
            return this.guarantorCustomerNumberField;
        }
        set
        {
            this.guarantorCustomerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string GuarantorCustomerName
    {
        get
        {
            return this.guarantorCustomerNameField;
        }
        set
        {
            this.guarantorCustomerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public decimal GuarantorAmount
    {
        get
        {
            return this.guarantorAmountField;
        }
        set
        {
            this.guarantorAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string GuarantorCurrencyCode
    {
        get
        {
            return this.guarantorCurrencyCodeField;
        }
        set
        {
            this.guarantorCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public System.DateTime GuarantorEntranceDate
    {
        get
        {
            return this.guarantorEntranceDateField;
        }
        set
        {
            this.guarantorEntranceDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public System.DateTime GuarantorEndDate
    {
        get
        {
            return this.guarantorEndDateField;
        }
        set
        {
            this.guarantorEndDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string GuarantorExtensionFlag
    {
        get
        {
            return this.guarantorExtensionFlagField;
        }
        set
        {
            this.guarantorExtensionFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public System.DateTime GuarantorExtensionDate
    {
        get
        {
            return this.guarantorExtensionDateField;
        }
        set
        {
            this.guarantorExtensionDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string ResponsibleOldDebtFlag
    {
        get
        {
            return this.responsibleOldDebtFlagField;
        }
        set
        {
            this.responsibleOldDebtFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string GuarantorRucuExists
    {
        get
        {
            return this.guarantorRucuExistsField;
        }
        set
        {
            this.guarantorRucuExistsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public System.DateTime GuarantorRucuDate
    {
        get
        {
            return this.guarantorRucuDateField;
        }
        set
        {
            this.guarantorRucuDateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementDocumentEntity
{

    private int engagementTranBranchCodeField;

    private int customerNumberField;

    private int engagementRefField;

    private long documentNumberField;

    private string rowStatusField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int EngagementTranBranchCode
    {
        get
        {
            return this.engagementTranBranchCodeField;
        }
        set
        {
            this.engagementTranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public long DocumentNumber
    {
        get
        {
            return this.documentNumberField;
        }
        set
        {
            this.documentNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string RowStatus
    {
        get
        {
            return this.rowStatusField;
        }
        set
        {
            this.rowStatusField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EngagementEntity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EngagementEntityTvekeGrid))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EngagementWebResultEntity))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class ForaBaseEntity
{

    private ForaRecordInfo recordInfoField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public ForaRecordInfo RecordInfo
    {
        get
        {
            return this.recordInfoField;
        }
        set
        {
            this.recordInfoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class ForaRecordInfo
{

    private string channelCodeField;

    private string userCodeField;

    private string tranCodeField;

    private System.DateTime updateDateField;

    private bool isActiveField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string ChannelCode
    {
        get
        {
            return this.channelCodeField;
        }
        set
        {
            this.channelCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string UserCode
    {
        get
        {
            return this.userCodeField;
        }
        set
        {
            this.userCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string TranCode
    {
        get
        {
            return this.tranCodeField;
        }
        set
        {
            this.tranCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public System.DateTime UpdateDate
    {
        get
        {
            return this.updateDateField;
        }
        set
        {
            this.updateDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public bool IsActive
    {
        get
        {
            return this.isActiveField;
        }
        set
        {
            this.isActiveField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementWebResultEntity : ForaBaseEntity
{

    private EngagementSuffixEntity[] engagementDetailField;

    private int customerNumberField;

    private string customerNameField;

    private int engagementRefField;

    private int tranBranchCodeField;

    private System.DateTime engagementDateField;

    private System.DateTime tranDateField;

    private decimal engagementAmountField;

    private string engagementCurrencyCodeField;

    private string taxFlagField;

    private string engagementTypeField;

    private int mainEngagementNumField;

    private string guarantorFlagField;

    private string engagementKindField;

    private string engagementKindCodeField;

    private string lastUpdatingChannelCodeField;

    private string lastUpdatingTranCodeField;

    private string lastUpdatingUserCodeField;

    private System.DateTime lastUpdateDateField;

    private string recordStatusField;

    private string explanationField;

    private EngagementGuarantorEntity[] engagementGuarantorsField;

    private string standartEngagementFormatField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
    public EngagementSuffixEntity[] EngagementDetail
    {
        get
        {
            return this.engagementDetailField;
        }
        set
        {
            this.engagementDetailField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string CustomerName
    {
        get
        {
            return this.customerNameField;
        }
        set
        {
            this.customerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public int TranBranchCode
    {
        get
        {
            return this.tranBranchCodeField;
        }
        set
        {
            this.tranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public System.DateTime EngagementDate
    {
        get
        {
            return this.engagementDateField;
        }
        set
        {
            this.engagementDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public System.DateTime TranDate
    {
        get
        {
            return this.tranDateField;
        }
        set
        {
            this.tranDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public decimal EngagementAmount
    {
        get
        {
            return this.engagementAmountField;
        }
        set
        {
            this.engagementAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string EngagementCurrencyCode
    {
        get
        {
            return this.engagementCurrencyCodeField;
        }
        set
        {
            this.engagementCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string TaxFlag
    {
        get
        {
            return this.taxFlagField;
        }
        set
        {
            this.taxFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string EngagementType
    {
        get
        {
            return this.engagementTypeField;
        }
        set
        {
            this.engagementTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public int MainEngagementNum
    {
        get
        {
            return this.mainEngagementNumField;
        }
        set
        {
            this.mainEngagementNumField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string GuarantorFlag
    {
        get
        {
            return this.guarantorFlagField;
        }
        set
        {
            this.guarantorFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string EngagementKind
    {
        get
        {
            return this.engagementKindField;
        }
        set
        {
            this.engagementKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string EngagementKindCode
    {
        get
        {
            return this.engagementKindCodeField;
        }
        set
        {
            this.engagementKindCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public string LastUpdatingChannelCode
    {
        get
        {
            return this.lastUpdatingChannelCodeField;
        }
        set
        {
            this.lastUpdatingChannelCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public string LastUpdatingTranCode
    {
        get
        {
            return this.lastUpdatingTranCodeField;
        }
        set
        {
            this.lastUpdatingTranCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public string LastUpdatingUserCode
    {
        get
        {
            return this.lastUpdatingUserCodeField;
        }
        set
        {
            this.lastUpdatingUserCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
    public System.DateTime LastUpdateDate
    {
        get
        {
            return this.lastUpdateDateField;
        }
        set
        {
            this.lastUpdateDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
    public string Explanation
    {
        get
        {
            return this.explanationField;
        }
        set
        {
            this.explanationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 21)]
    public EngagementGuarantorEntity[] EngagementGuarantors
    {
        get
        {
            return this.engagementGuarantorsField;
        }
        set
        {
            this.engagementGuarantorsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
    public string StandartEngagementFormat
    {
        get
        {
            return this.standartEngagementFormatField;
        }
        set
        {
            this.standartEngagementFormatField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class ExpertiseReturnMessage
{

    private string cautionKindField;

    private decimal expertAmountField;

    private string expertCurrencyCodeField;

    private System.DateTime expertiseDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string CautionKind
    {
        get
        {
            return this.cautionKindField;
        }
        set
        {
            this.cautionKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public decimal ExpertAmount
    {
        get
        {
            return this.expertAmountField;
        }
        set
        {
            this.expertAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string ExpertCurrencyCode
    {
        get
        {
            return this.expertCurrencyCodeField;
        }
        set
        {
            this.expertCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public System.DateTime ExpertiseDate
    {
        get
        {
            return this.expertiseDateField;
        }
        set
        {
            this.expertiseDateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementInfo
{

    private int accountBranchCodeField;

    private int accountNumberField;

    private int accountSuffixField;

    private int engagementRefField;

    private string engagementCurrencyCodeField;

    private decimal engagementAmountField;

    private System.DateTime engagementDateField;

    private string engagementTypeField;

    private string engagementKindField;

    private string userCodeField;

    private long workflowActionNumberField;

    private int lineNumberField;

    private string oldRecordStatusField;

    private string recordStatusField;

    private EngagementGuarantorEntity[] engagementGuarantorsField;

    private EngagementDocumentEntity[] engagementDocumentsField;

    private bool isDigitalField;

    private string engagementVersionNoField;

    private int mainEngagementNumField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int AccountBranchCode
    {
        get
        {
            return this.accountBranchCodeField;
        }
        set
        {
            this.accountBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int AccountNumber
    {
        get
        {
            return this.accountNumberField;
        }
        set
        {
            this.accountNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public int AccountSuffix
    {
        get
        {
            return this.accountSuffixField;
        }
        set
        {
            this.accountSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string EngagementCurrencyCode
    {
        get
        {
            return this.engagementCurrencyCodeField;
        }
        set
        {
            this.engagementCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public decimal EngagementAmount
    {
        get
        {
            return this.engagementAmountField;
        }
        set
        {
            this.engagementAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public System.DateTime EngagementDate
    {
        get
        {
            return this.engagementDateField;
        }
        set
        {
            this.engagementDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string EngagementType
    {
        get
        {
            return this.engagementTypeField;
        }
        set
        {
            this.engagementTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public string EngagementKind
    {
        get
        {
            return this.engagementKindField;
        }
        set
        {
            this.engagementKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string UserCode
    {
        get
        {
            return this.userCodeField;
        }
        set
        {
            this.userCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public long WorkflowActionNumber
    {
        get
        {
            return this.workflowActionNumberField;
        }
        set
        {
            this.workflowActionNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public int LineNumber
    {
        get
        {
            return this.lineNumberField;
        }
        set
        {
            this.lineNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string OldRecordStatus
    {
        get
        {
            return this.oldRecordStatusField;
        }
        set
        {
            this.oldRecordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 14)]
    public EngagementGuarantorEntity[] EngagementGuarantors
    {
        get
        {
            return this.engagementGuarantorsField;
        }
        set
        {
            this.engagementGuarantorsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 15)]
    public EngagementDocumentEntity[] EngagementDocuments
    {
        get
        {
            return this.engagementDocumentsField;
        }
        set
        {
            this.engagementDocumentsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public bool IsDigital
    {
        get
        {
            return this.isDigitalField;
        }
        set
        {
            this.isDigitalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public string EngagementVersionNo
    {
        get
        {
            return this.engagementVersionNoField;
        }
        set
        {
            this.engagementVersionNoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
    public int MainEngagementNum
    {
        get
        {
            return this.mainEngagementNumField;
        }
        set
        {
            this.mainEngagementNumField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementResult
{

    private int referenceNumberField;

    private bool hasErrorField;

    private string errorMessageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int ReferenceNumber
    {
        get
        {
            return this.referenceNumberField;
        }
        set
        {
            this.referenceNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public bool HasError
    {
        get
        {
            return this.hasErrorField;
        }
        set
        {
            this.hasErrorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string ErrorMessage
    {
        get
        {
            return this.errorMessageField;
        }
        set
        {
            this.errorMessageField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class EngagementGuarantorInfo
{

    private int customerNumberField;

    private int engagementRefField;

    private string engagementKindField;

    private string engagementTypeField;

    private bool timeExceedingField;

    private bool amountExceedingField;

    private string standartEngagementFormatField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int EngagementRef
    {
        get
        {
            return this.engagementRefField;
        }
        set
        {
            this.engagementRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string EngagementKind
    {
        get
        {
            return this.engagementKindField;
        }
        set
        {
            this.engagementKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string EngagementType
    {
        get
        {
            return this.engagementTypeField;
        }
        set
        {
            this.engagementTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public bool TimeExceeding
    {
        get
        {
            return this.timeExceedingField;
        }
        set
        {
            this.timeExceedingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public bool AmountExceeding
    {
        get
        {
            return this.amountExceedingField;
        }
        set
        {
            this.amountExceedingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string StandartEngagementFormat
    {
        get
        {
            return this.standartEngagementFormatField;
        }
        set
        {
            this.standartEngagementFormatField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class GroupCollateralWebResultEntity
{

    private int cautionsRefField;

    private string cautionKindField;

    private string cautionKindTextField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int CautionsRef
    {
        get
        {
            return this.cautionsRefField;
        }
        set
        {
            this.cautionsRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string CautionKind
    {
        get
        {
            return this.cautionKindField;
        }
        set
        {
            this.cautionKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string CautionKindText
    {
        get
        {
            return this.cautionKindTextField;
        }
        set
        {
            this.cautionKindTextField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class CautionsSuffixProductEntity
{

    private int cautionCreditSuffixField;

    private string creditTypeField;

    private string superProductCodeField;

    private string productCodeField;

    private string projectCodeField;

    private string campaignCodeField;

    private string cautionCurrencyTypeField;

    private decimal cautionCreditProductAmountField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public int CautionCreditSuffix
    {
        get
        {
            return this.cautionCreditSuffixField;
        }
        set
        {
            this.cautionCreditSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string CreditType
    {
        get
        {
            return this.creditTypeField;
        }
        set
        {
            this.creditTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string SuperProductCode
    {
        get
        {
            return this.superProductCodeField;
        }
        set
        {
            this.superProductCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string ProductCode
    {
        get
        {
            return this.productCodeField;
        }
        set
        {
            this.productCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string ProjectCode
    {
        get
        {
            return this.projectCodeField;
        }
        set
        {
            this.projectCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string CampaignCode
    {
        get
        {
            return this.campaignCodeField;
        }
        set
        {
            this.campaignCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string CautionCurrencyType
    {
        get
        {
            return this.cautionCurrencyTypeField;
        }
        set
        {
            this.cautionCurrencyTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public decimal CautionCreditProductAmount
    {
        get
        {
            return this.cautionCreditProductAmountField;
        }
        set
        {
            this.cautionCreditProductAmountField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://core.intertech.com.tr/")]
public partial class ColleteralWebResultEntity
{

    private CautionsSuffixProductEntity[] cautionCreditDetailField;

    private int cautionTranBranchCodeField;

    private int cautionsRefField;

    private string cautionKindField;

    private string cautionKindTextField;

    private string cautionKindDefinitionField;

    private System.DateTime cautionTakenDateField;

    private System.DateTime cautionTranDateField;

    private int creditBranchCodeField;

    private int customerNumberField;

    private string customerNameField;

    private decimal cautionAmountField;

    private string cautionCurrencyCodeField;

    private string cautionCurrencyNameField;

    private string cautionExplanationField;

    private string cautionTypeField;

    private int firstSuffixField;

    private string cautionGroupFlagField;

    private int cautionGroupCodeField;

    private string cautionGroupNameField;

    private string allGroupCustomerFlagField;

    private string recordStatusField;

    private string userCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
    public CautionsSuffixProductEntity[] CautionCreditDetail
    {
        get
        {
            return this.cautionCreditDetailField;
        }
        set
        {
            this.cautionCreditDetailField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public int CautionTranBranchCode
    {
        get
        {
            return this.cautionTranBranchCodeField;
        }
        set
        {
            this.cautionTranBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public int CautionsRef
    {
        get
        {
            return this.cautionsRefField;
        }
        set
        {
            this.cautionsRefField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string CautionKind
    {
        get
        {
            return this.cautionKindField;
        }
        set
        {
            this.cautionKindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string CautionKindText
    {
        get
        {
            return this.cautionKindTextField;
        }
        set
        {
            this.cautionKindTextField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string CautionKindDefinition
    {
        get
        {
            return this.cautionKindDefinitionField;
        }
        set
        {
            this.cautionKindDefinitionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public System.DateTime CautionTakenDate
    {
        get
        {
            return this.cautionTakenDateField;
        }
        set
        {
            this.cautionTakenDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public System.DateTime CautionTranDate
    {
        get
        {
            return this.cautionTranDateField;
        }
        set
        {
            this.cautionTranDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public int CreditBranchCode
    {
        get
        {
            return this.creditBranchCodeField;
        }
        set
        {
            this.creditBranchCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public int CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string CustomerName
    {
        get
        {
            return this.customerNameField;
        }
        set
        {
            this.customerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public decimal CautionAmount
    {
        get
        {
            return this.cautionAmountField;
        }
        set
        {
            this.cautionAmountField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string CautionCurrencyCode
    {
        get
        {
            return this.cautionCurrencyCodeField;
        }
        set
        {
            this.cautionCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string CautionCurrencyName
    {
        get
        {
            return this.cautionCurrencyNameField;
        }
        set
        {
            this.cautionCurrencyNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
    public string CautionExplanation
    {
        get
        {
            return this.cautionExplanationField;
        }
        set
        {
            this.cautionExplanationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public string CautionType
    {
        get
        {
            return this.cautionTypeField;
        }
        set
        {
            this.cautionTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public int FirstSuffix
    {
        get
        {
            return this.firstSuffixField;
        }
        set
        {
            this.firstSuffixField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public string CautionGroupFlag
    {
        get
        {
            return this.cautionGroupFlagField;
        }
        set
        {
            this.cautionGroupFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
    public int CautionGroupCode
    {
        get
        {
            return this.cautionGroupCodeField;
        }
        set
        {
            this.cautionGroupCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
    public string CautionGroupName
    {
        get
        {
            return this.cautionGroupNameField;
        }
        set
        {
            this.cautionGroupNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
    public string AllGroupCustomerFlag
    {
        get
        {
            return this.allGroupCustomerFlagField;
        }
        set
        {
            this.allGroupCustomerFlagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
    public string RecordStatus
    {
        get
        {
            return this.recordStatusField;
        }
        set
        {
            this.recordStatusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
    public string UserCode
    {
        get
        {
            return this.userCodeField;
        }
        set
        {
            this.userCodeField = value;
        }
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface ColleteralSoapChannel : ColleteralSoap, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class ColleteralSoapClient : System.ServiceModel.ClientBase<ColleteralSoap>, ColleteralSoap
{

    public ColleteralSoapClient()
    {
    }

    // public ColleteralSoapClient(string endpointConfigurationName) :
    //         base(endpointConfigurationName)
    // {
    // }

    // public ColleteralSoapClient(string endpointConfigurationName, string remoteAddress) :
    //         base(endpointConfigurationName, remoteAddress)
    // {
    // }

    // public ColleteralSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
    //         base(endpointConfigurationName, remoteAddress)
    // {
    // }

    public ColleteralSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public void InsertRepurchasePaymentAndExpertise(long WfInstanceId, int CautionBranchCode, int CautionRefNum, string ChannelCode, string TranCode, string UserCode, System.Data.DataSet Entity)
    {
        base.Channel.InsertRepurchasePaymentAndExpertise(WfInstanceId, CautionBranchCode, CautionRefNum, ChannelCode, TranCode, UserCode, Entity);
    }

    public System.Threading.Tasks.Task InsertRepurchasePaymentAndExpertiseAsync(long WfInstanceId, int CautionBranchCode, int CautionRefNum, string ChannelCode, string TranCode, string UserCode, System.Data.DataSet Entity)
    {
        return base.Channel.InsertRepurchasePaymentAndExpertiseAsync(WfInstanceId, CautionBranchCode, CautionRefNum, ChannelCode, TranCode, UserCode, Entity);
    }

    public GuaranteeReturnMessage[] GetGuaranteeLetterWebService(short CreditAccountBranchCode, int CreditAccountNumber, int CreditAccountSuffix)
    {
        return base.Channel.GetGuaranteeLetterWebService(CreditAccountBranchCode, CreditAccountNumber, CreditAccountSuffix);
    }

    public System.Threading.Tasks.Task<GuaranteeReturnMessage[]> GetGuaranteeLetterWebServiceAsync(short CreditAccountBranchCode, int CreditAccountNumber, int CreditAccountSuffix)
    {
        return base.Channel.GetGuaranteeLetterWebServiceAsync(CreditAccountBranchCode, CreditAccountNumber, CreditAccountSuffix);
    }

    public ColleteralWebResultEntity[] GetCautionInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetCautionInfo(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public System.Threading.Tasks.Task<ColleteralWebResultEntity[]> GetCautionInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetCautionInfoAsync(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public GroupCollateralWebResultEntity[] GetGroupCautionReferenceList(int BranchCode, int GroupCode, long CustomerNo)
    {
        return base.Channel.GetGroupCautionReferenceList(BranchCode, GroupCode, CustomerNo);
    }

    public System.Threading.Tasks.Task<GroupCollateralWebResultEntity[]> GetGroupCautionReferenceListAsync(int BranchCode, int GroupCode, long CustomerNo)
    {
        return base.Channel.GetGroupCautionReferenceListAsync(BranchCode, GroupCode, CustomerNo);
    }

    public EngagementWebResultEntity[] GetEngagementInfoForRota(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetEngagementInfoForRota(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public System.Threading.Tasks.Task<EngagementWebResultEntity[]> GetEngagementInfoForRotaAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetEngagementInfoForRotaAsync(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public EngagementEntity GetEngagementActiveOrPassive(int customerNumber, int engagementRef)
    {
        return base.Channel.GetEngagementActiveOrPassive(customerNumber, engagementRef);
    }

    public System.Threading.Tasks.Task<EngagementEntity> GetEngagementActiveOrPassiveAsync(int customerNumber, int engagementRef)
    {
        return base.Channel.GetEngagementActiveOrPassiveAsync(customerNumber, engagementRef);
    }

    public EngagementWebResultEntity[] GetEngagementInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetEngagementInfo(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public System.Threading.Tasks.Task<EngagementWebResultEntity[]> GetEngagementInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetEngagementInfoAsync(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public EngagementGuarantorInfo[] GetEngagementGuarantorExceedingInfo(int BranchCode, int CustomerNo)
    {
        return base.Channel.GetEngagementGuarantorExceedingInfo(BranchCode, CustomerNo);
    }

    public System.Threading.Tasks.Task<EngagementGuarantorInfo[]> GetEngagementGuarantorExceedingInfoAsync(int BranchCode, int CustomerNo)
    {
        return base.Channel.GetEngagementGuarantorExceedingInfoAsync(BranchCode, CustomerNo);
    }

    public System.Data.DataSet GetGuarantorInfo(int QueryKey, string QueryType, string ChannelCode, string LanguageCode)
    {
        return base.Channel.GetGuarantorInfo(QueryKey, QueryType, ChannelCode, LanguageCode);
    }

    public System.Threading.Tasks.Task<System.Data.DataSet> GetGuarantorInfoAsync(int QueryKey, string QueryType, string ChannelCode, string LanguageCode)
    {
        return base.Channel.GetGuarantorInfoAsync(QueryKey, QueryType, ChannelCode, LanguageCode);
    }

    public string[] CheckCustomerAndGurantorCreditsForRetailCreditWF(long customerNo, long[] workflowGuarantorCustomers)
    {
        return base.Channel.CheckCustomerAndGurantorCreditsForRetailCreditWF(customerNo, workflowGuarantorCustomers);
    }

    public System.Threading.Tasks.Task<string[]> CheckCustomerAndGurantorCreditsForRetailCreditWFAsync(long customerNo, long[] workflowGuarantorCustomers)
    {
        return base.Channel.CheckCustomerAndGurantorCreditsForRetailCreditWFAsync(customerNo, workflowGuarantorCustomers);
    }

    public void GetCautionsDepositAccountEODList()
    {
        base.Channel.GetCautionsDepositAccountEODList();
    }

    public System.Threading.Tasks.Task GetCautionsDepositAccountEODListAsync()
    {
        return base.Channel.GetCautionsDepositAccountEODListAsync();
    }

    public void UpdateCaution_NA_AT()
    {
        base.Channel.UpdateCaution_NA_AT();
    }

    public System.Threading.Tasks.Task UpdateCaution_NA_ATAsync()
    {
        return base.Channel.UpdateCaution_NA_ATAsync();
    }

    public void GetCautionsHSEODList()
    {
        base.Channel.GetCautionsHSEODList();
    }

    public System.Threading.Tasks.Task GetCautionsHSEODListAsync()
    {
        return base.Channel.GetCautionsHSEODListAsync();
    }

    public void GetCautionsNOVAEODList()
    {
        base.Channel.GetCautionsNOVAEODList();
    }

    public System.Threading.Tasks.Task GetCautionsNOVAEODListAsync()
    {
        return base.Channel.GetCautionsNOVAEODListAsync();
    }

    public void GetLetterCommisionList()
    {
        base.Channel.GetLetterCommisionList();
    }

    public System.Threading.Tasks.Task GetLetterCommisionListAsync()
    {
        return base.Channel.GetLetterCommisionListAsync();
    }

    public EngagementResult DoAutomaticEngagement(int accountBranchCode, int accountNumber, int accountSuffix, System.DateTime engagementDate, string engagementType, string userCode)
    {
        return base.Channel.DoAutomaticEngagement(accountBranchCode, accountNumber, accountSuffix, engagementDate, engagementType, userCode);
    }

    public System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementAsync(int accountBranchCode, int accountNumber, int accountSuffix, System.DateTime engagementDate, string engagementType, string userCode)
    {
        return base.Channel.DoAutomaticEngagementAsync(accountBranchCode, accountNumber, accountSuffix, engagementDate, engagementType, userCode);
    }

    public EngagementResult DoAutomaticEngagementWithGuarantor(EngagementInfo engagement)
    {
        return base.Channel.DoAutomaticEngagementWithGuarantor(engagement);
    }

    public System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementWithGuarantorAsync(EngagementInfo engagement)
    {
        return base.Channel.DoAutomaticEngagementWithGuarantorAsync(engagement);
    }

    public EngagementResult DoAutomaticEngagementPlain(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode)
    {
        return base.Channel.DoAutomaticEngagementPlain(accountBranchCode, accountNumber, accountSuffix, currencyCode, engagementDate, engagementType, engagementKind, engagementAmount, userCode);
    }

    public System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementPlainAsync(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode)
    {
        return base.Channel.DoAutomaticEngagementPlainAsync(accountBranchCode, accountNumber, accountSuffix, currencyCode, engagementDate, engagementType, engagementKind, engagementAmount, userCode);
    }

    public EngagementResult DoAutomaticEngagementPlainWithDocumentNo(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode, string engagementVersionNo, int mainEngagementNum)
    {
        return base.Channel.DoAutomaticEngagementPlainWithDocumentNo(accountBranchCode, accountNumber, accountSuffix, currencyCode, engagementDate, engagementType, engagementKind, engagementAmount, userCode, engagementVersionNo, mainEngagementNum);
    }

    public System.Threading.Tasks.Task<EngagementResult> DoAutomaticEngagementPlainWithDocumentNoAsync(int accountBranchCode, int accountNumber, int accountSuffix, string currencyCode, System.DateTime engagementDate, string engagementType, string engagementKind, decimal engagementAmount, string userCode, string engagementVersionNo, int mainEngagementNum)
    {
        return base.Channel.DoAutomaticEngagementPlainWithDocumentNoAsync(accountBranchCode, accountNumber, accountSuffix, currencyCode, engagementDate, engagementType, engagementKind, engagementAmount, userCode, engagementVersionNo, mainEngagementNum);
    }

    public ExpertiseReturnMessage[] GetExpertiseInfo(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetExpertiseInfo(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public System.Threading.Tasks.Task<ExpertiseReturnMessage[]> GetExpertiseInfoAsync(int BranchCode, int QueryKey, string QueryType, string LanguageCode)
    {
        return base.Channel.GetExpertiseInfoAsync(BranchCode, QueryKey, QueryType, LanguageCode);
    }

    public EngagementEntityTveke GetTavke(long guarantorCustomer)
    {
        return base.Channel.GetTavke(guarantorCustomer);
    }

    public System.Threading.Tasks.Task<EngagementEntityTveke> GetTavkeAsync(long guarantorCustomer)
    {
        return base.Channel.GetTavkeAsync(guarantorCustomer);
    }

    public EngagementEntityTalke GetTalke(long guarantorCustomer)
    {
        return base.Channel.GetTalke(guarantorCustomer);
    }

    public System.Threading.Tasks.Task<EngagementEntityTalke> GetTalkeAsync(long guarantorCustomer)
    {
        return base.Channel.GetTalkeAsync(guarantorCustomer);
    }

    public System.Data.DataTable CreateLifeColleteral(
                int branchCode,
                string arrangedInsuranceHand,
                decimal cautionAmount,
                string cautionCurrencyCode,
                string cautionCurrencyName,
                decimal insuranceAmount,
                int insuranceContactNumber,
                string insuranceCurrencyCode,
                System.DateTime insuranceMaturity,
                long policyNumber,
                string policyType,
                int policyZeylNo,
                int creditBranchCode,
                int creditSuffix,
                int customerNumber,
                string insCompany,
                long WFInstanceID,
                string loginName)
    {
        return base.Channel.CreateLifeColleteral(branchCode, arrangedInsuranceHand, cautionAmount, cautionCurrencyCode, cautionCurrencyName, insuranceAmount, insuranceContactNumber, insuranceCurrencyCode, insuranceMaturity, policyNumber, policyType, policyZeylNo, creditBranchCode, creditSuffix, customerNumber, insCompany, WFInstanceID, loginName);
    }

    public System.Threading.Tasks.Task<System.Data.DataTable> CreateLifeColleteralAsync(
                int branchCode,
                string arrangedInsuranceHand,
                decimal cautionAmount,
                string cautionCurrencyCode,
                string cautionCurrencyName,
                decimal insuranceAmount,
                int insuranceContactNumber,
                string insuranceCurrencyCode,
                System.DateTime insuranceMaturity,
                long policyNumber,
                string policyType,
                int policyZeylNo,
                int creditBranchCode,
                int creditSuffix,
                int customerNumber,
                string insCompany,
                long WFInstanceID,
                string loginName)
    {
        return base.Channel.CreateLifeColleteralAsync(branchCode, arrangedInsuranceHand, cautionAmount, cautionCurrencyCode, cautionCurrencyName, insuranceAmount, insuranceContactNumber, insuranceCurrencyCode, insuranceMaturity, policyNumber, policyType, policyZeylNo, creditBranchCode, creditSuffix, customerNumber, insCompany, WFInstanceID, loginName);
    }

    public object InsuranceTakenCautionInsuranceInfoInsert(int CustomerNo, int PayerCustomerNo, long PolicyNo, string primAmount, string cautionAmount, int creditRefNumber, int cautionRefNumber, int accountingRefNo, string productCode, System.DateTime policyEndDate, string userName, string bransCode, string isPayer)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoInsert(CustomerNo, PayerCustomerNo, PolicyNo, primAmount, cautionAmount, creditRefNumber, cautionRefNumber, accountingRefNo, productCode, policyEndDate, userName, bransCode, isPayer);
    }

    public System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoInsertAsync(int CustomerNo, int PayerCustomerNo, long PolicyNo, string primAmount, string cautionAmount, int creditRefNumber, int cautionRefNumber, int accountingRefNo, string productCode, System.DateTime policyEndDate, string userName, string bransCode, string isPayer)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoInsertAsync(CustomerNo, PayerCustomerNo, PolicyNo, primAmount, cautionAmount, creditRefNumber, cautionRefNumber, accountingRefNo, productCode, policyEndDate, userName, bransCode, isPayer);
    }

    public object InsuranceTakenCautionInsuranceInfoInsertCekOrTeminat(
                int CustomerNo,
                int PayerCustomerNo,
                long PolicyNo,
                string primAmount,
                string cautionAmount,
                int creditRefNumber,
                int cautionRefNumber,
                int accountingRefNo,
                string productCode,
                System.DateTime policyEndDate,
                string userName,
                string bransCode,
                int branchCode,
                int accountNumber,
                int accountSuffix,
                string genLedgerType,
                string isPayer)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoInsertCekOrTeminat(CustomerNo, PayerCustomerNo, PolicyNo, primAmount, cautionAmount, creditRefNumber, cautionRefNumber, accountingRefNo, productCode, policyEndDate, userName, bransCode, branchCode, accountNumber, accountSuffix, genLedgerType, isPayer);
    }

    public System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoInsertCekOrTeminatAsync(
                int CustomerNo,
                int PayerCustomerNo,
                long PolicyNo,
                string primAmount,
                string cautionAmount,
                int creditRefNumber,
                int cautionRefNumber,
                int accountingRefNo,
                string productCode,
                System.DateTime policyEndDate,
                string userName,
                string bransCode,
                int branchCode,
                int accountNumber,
                int accountSuffix,
                string genLedgerType,
                string isPayer)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoInsertCekOrTeminatAsync(CustomerNo, PayerCustomerNo, PolicyNo, primAmount, cautionAmount, creditRefNumber, cautionRefNumber, accountingRefNo, productCode, policyEndDate, userName, bransCode, branchCode, accountNumber, accountSuffix, genLedgerType, isPayer);
    }

    public object InsuranceTakenCautionInsuranceInfoCancel(long PolicyNo)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoCancel(PolicyNo);
    }

    public System.Threading.Tasks.Task<object> InsuranceTakenCautionInsuranceInfoCancelAsync(long PolicyNo)
    {
        return base.Channel.InsuranceTakenCautionInsuranceInfoCancelAsync(PolicyNo);
    }

    public System.Data.DataTable GetInsuranceCustomerLoans(int customerNo)
    {
        return base.Channel.GetInsuranceCustomerLoans(customerNo);
    }

    public System.Threading.Tasks.Task<System.Data.DataTable> GetInsuranceCustomerLoansAsync(int customerNo)
    {
        return base.Channel.GetInsuranceCustomerLoansAsync(customerNo);
    }

    public GetGuaranteeLetterInfoResponse[] GetGuaranteeLetterInfo(string CitizenshipNumber, long ExternalClientNo)
    {
        return base.Channel.GetGuaranteeLetterInfo(CitizenshipNumber, ExternalClientNo);
    }

    public System.Threading.Tasks.Task<GetGuaranteeLetterInfoResponse[]> GetGuaranteeLetterInfoAsync(string CitizenshipNumber, long ExternalClientNo)
    {
        return base.Channel.GetGuaranteeLetterInfoAsync(CitizenshipNumber, ExternalClientNo);
    }

    public bool IsNewCustomer(int accountNumber)
    {
        return base.Channel.IsNewCustomer(accountNumber);
    }

    public System.Threading.Tasks.Task<bool> IsNewCustomerAsync(int accountNumber)
    {
        return base.Channel.IsNewCustomerAsync(accountNumber);
    }

    public System.Data.DataSet GetColleteral(int branchCode, int cautionRef, bool withSecurity)
    {
        return base.Channel.GetColleteral(branchCode, cautionRef, withSecurity);
    }

    public System.Threading.Tasks.Task<System.Data.DataSet> GetColleteralAsync(int branchCode, int cautionRef, bool withSecurity)
    {
        return base.Channel.GetColleteralAsync(branchCode, cautionRef, withSecurity);
    }

    public RepurchasePreviousRecords[] GetRepurchaseRecords(string BranchCode, string RefNum)
    {
        return base.Channel.GetRepurchaseRecords(BranchCode, RefNum);
    }

    public System.Threading.Tasks.Task<RepurchasePreviousRecords[]> GetRepurchaseRecordsAsync(string BranchCode, string RefNum)
    {
        return base.Channel.GetRepurchaseRecordsAsync(BranchCode, RefNum);
    }

    public RepurchaseSelectedRecords GetRepurchaseSelection(string BranchCode, string RefNum, string IndependentSection, string RepurchaseRecordStatus)
    {
        return base.Channel.GetRepurchaseSelection(BranchCode, RefNum, IndependentSection, RepurchaseRecordStatus);
    }

    public System.Threading.Tasks.Task<RepurchaseSelectedRecords> GetRepurchaseSelectionAsync(string BranchCode, string RefNum, string IndependentSection, string RepurchaseRecordStatus)
    {
        return base.Channel.GetRepurchaseSelectionAsync(BranchCode, RefNum, IndependentSection, RepurchaseRecordStatus);
    }

    public void RepurchaseWorkflowBatch()
    {
        base.Channel.RepurchaseWorkflowBatch();
    }

    public System.Threading.Tasks.Task RepurchaseWorkflowBatchAsync()
    {
        return base.Channel.RepurchaseWorkflowBatchAsync();
    }

    public void RepurchaseAccounting(RepurchaseAccountingRecord record)
    {
        base.Channel.RepurchaseAccounting(record);
    }

    public System.Threading.Tasks.Task RepurchaseAccountingAsync(RepurchaseAccountingRecord record)
    {
        return base.Channel.RepurchaseAccountingAsync(record);
    }
}
