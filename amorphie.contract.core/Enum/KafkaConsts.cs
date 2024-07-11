namespace amorphie.contract.core.Enum;

public static class KafkaConsts
{
    public const string KafkaName = "contract-pubsub-kafka";
    public const string SendDocumentInstanceDataToDYSTopicName = "contract-to-dys-document";
    public const string SendEngagementDataToTSIZLTopicName = "contract-to-tsizl-engagement";

    #region Migration

    public const string DysDocument = "CONTRACT.CALLBACK.dys-document";
    public const string DysDocumentTag = "CONTRACT.CALLBACK.dys-document-tag";


    #endregion

}
