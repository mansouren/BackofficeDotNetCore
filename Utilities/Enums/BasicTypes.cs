using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Enums
{
    public enum BasicType
    {
        StatusCode = 1,
        MessageTypeCode = 2,
        GenderTypeCode = 3,
        TitleTypeCode = 4,
        PartyNatureCode = 5,
        IncludedPartyNatureCode = 6,
        MaritalStatusCode = 7,
        EducationLevelCode = 8,
        ContactInfoTypeCode = 9,
        SupportingActivityTypeCode = 10,
        SettlementTypeCode = 12,
        TerminalSerivceTypeCode = 13,
        VisitResultTypeCode = 14,
        EMRejectReasonTypeCode = 15,
        PMRejectReasonTypeCode = 16,
        MaintenanceTypeCode = 17,
        MaintenanceItemTypeCode = 18,
        LegalNatureCode = 21,
        JournalStatusCode = 22,
        JournalTypeCode = 23,
        ReverseStatusCode = 24,
        SettlementStatusCode = 25,
        TerminalTypeCode = 26,
        PrCode = 27,
        SwAccountTypeCode = 28,
        ExternalReasonCode = 29,
        RasteBazar = 30,
        TerminalStatus = 31,
        DeviceType = 32,
        Severity = 33,
        PripheralType = 34,
        UserActions = 39,
        SPrCode = 41,
        FestivalType = 42,
        PaymentType = 49,
        SecurityModel = 44,
        AccessType = 45,
        UserType = 46,
        ServiceCode = 47,
        CardOperationType = 51,
        AddDocumentErrors = 52,
        ISSTXNType = 53,
        MessageSendType = 13,
        Prefix1 = 79,
        Prefix2 = 80,
        GroupAction = 81,
        ErrorFile = 82,
        IssuerActionGroup = 83,
        SuspendIdentifier = 50,
        EmbossCardType = 84,
        ContactType = 85,
        ChangeCardNumber = 86,
        ChargeStatus = 87,
        BatchStatus = 89,
        BatchRecordStatus = 90,
        BatchModeType = 91,
        Table = 92,
        DepositType = 94,
        CardNAture = 96,
        ChargeDetailStatus = 97,
        CardRequest = 98,
        CardRequestStatus = 99,
        PriorityRequest = 100,
        SendFromCardRequest = 102,
        ErrorKind = 104,
        FileStatusCode = 77,
        DepositReportStatus = 105,
        TransactionNetwork = 106,

        EnvelopeStatus = 112,
        PrintSortOrder = 113,

        DocumentRegisterType = 121
    }

    public class BasicValue
    {
        public enum FeeType
        {
            Cashe = 0,
            FromCardHolder = 1
        }

        public enum FeeAccountTitles
        {
            CostCenter = 0,
            FeeSodor = 1,
            FeeSodorRonevesht = 2,
            FeeTamdid = 3,
            FeeFaalsazi = 4,
            FeeEnsedad = 5,
            FeeRafeEnsedad = 6,
            FeeRamzeAval = 7,
            FeeRamzDovom = 8,
            FeeSms = 9
        }

        public enum BOLanguage
        {
            Persian = 0,
            English = 1,
            Arabic = 2
        }
        public enum PrintSortOrder
        {
            CardNoAndPersonal = 1,
            CardRequestAndPersonal = 2,
            IndicatorAndCardNo = 3,
            CardNo = 4,
            ProductCode = 5
        }

        public enum EnvelopeStatus
        {
            CloseDoor = 1,
            OpenDoor = 2
        }

        public enum TxnType
        {
            Purchase = 0,
            Withdrawal = 1,
            Bill = 17,
            Balance = 31,
            CheckBalanceAmount = 33,
            EnteghalBein2Card = 40,
            EnteghalAz = 46,
            EnteghalBe = 47,
            ChangeSecPass = 87,
            ChangePass = 97
        }

        public enum BillChargeType
        {
            Kadona = 20,
            GitCardMall = 24
        }

        public enum TransactionNetwork
        {
            Shetabi = 1,
            Virtual = 999999
        }
        public enum DepositReportStatus
        {
            PerparingInformation = 1,
            CalculatingInformation = 2,
            PerparingFile = 3,
            Done = 4
        }
        public enum UserToken
        {
            Reserved = 0,
            Used = 1
        }
        public enum SaleConflictChargeStatus
        {
            NoChargeFound = -1,
            Duplicate = -2
        }

        public enum ErrorKind
        {
            Error = 0,
            Warning = 1
        }

        public enum SendFromCardRequest
        {
            FromBackOffice = 1,
            FromPecCounter = 2,
            FromPortal = 3,
            FromKioskWebService = 4,
            FromService = 5,
            FromTopCard = 6
        }

        public enum PriorityRequest
        {
            Immediate = 1,
            Momentary = 2,
            Normal = 3,
        }
        public enum CardDetailStatus
        {
            WaitingToDone = 0,
            Doing = 1,
            Done = 2,
            Error = 3,
            ReBuildEmbosser = 4,
            BulkDone = 10,
            DeleteDetail = -16
        }

        public enum ChargeDetailStatus
        {
            WaitingToCharge = 0,
            Charging = 1,
            Done = 2,
            Error = 3,
            ReCreating = 4,
            SetChargingStatusError = 5,
            SetDoneStatusError = 6,
            SaveDocumentIdError = 7,
            WaitingCreateFile = 10,
            Duplicate = -15
        }
        public enum CardNature
        {
            Debit = 1,
            Credit = 2,
            Gift = 3,
            PrePaid = 4
        }
        public enum HamkaranSystem
        {
            Draft = 0,
            LoadingData = 1,
            GeneratingFile = 2,
            Done = 3
        }

        public enum DepositType
        {
            Cash = 0,
            Commitment = 1,
            Credits = 2,
            DeductedCardBalance = 4,
            InternetPayment = 5
        }

        public enum Table
        {
            None = -1,
            AcqCardAcceptor = 0,
            AcqTerminal = 1,
            ACTBatchTerminal = 2,
            Customer = 3,
            ACTBatchBankAccount = 4,
            ACTBatchCardAcceptor = 5,
            Acceptor = 6,
            Terminal = 7,
            BankAccount = 8
        }

        public enum ChargeStatus
        {
            CreateChargeFile = -13,
            ProcessChargeFile = -12,

            CreateCardExtensionFile = -11,
            ProcessCardExtensionFile = -10,

            CreateIssueGiftCardFile = -9,
            ProcessIssueGiftCardFile = -8,

            WaitingToAcceptProcessingFile = -7,
            ProcessDefineCustomerFile = -6,
            DefineCustomer = -5,
            Offline = -3,
            Processing = -2,
            ChashCharge = -1,
            Draft = 1,
            WaitingToApprove = 2,
            WaitingAssigning = 3,
            Done = 4,
            Suspend = 5,
            Doing = 6,
            Error = 7,
            SendingFromWebService = 8,
            ErrorCompleting = 9,
            ErrorProcessing = 10,
            DoneWithError = 11,
            AssingingCard = 12,
            WaitingDownload = 13,
            WaitingActivation = 14,
            WaitingPasswordPrint = 15,
            DraftBranch = 16,
            WaitingToApproveBranch = 17,
            DoingBranch = 18,
            WaitingToPrintCardBranch = 19,
            WaitingToPrintPasswordBranch = 20,
            WaitingToActivationBranch = 21,
            ErrorInCallingPortalWebService = 22,
            ReCreatingEmbosserFile = 23,
            WaittingToCheckUserForMessageOrPicture = 24,
            PrintAmountOnCard = 25,

            Service = 110
        }

        public enum ChangeCardNumber
        {
            WithChange = 1,
            WithoutChange = 2
        }
        public enum SuspendIdentifier
        {
            Moshtari = 1,
            Karshenas = 2,
            Tamdid = 3
        }
        public enum ErrorFile
        {
            LineLengthError = 1,
            LineDuplicate = 2,
            NoTerminal = 3,
            DuplicateTerminal = 4,
            DuplicateAcceptor = 5,
            NoAcceptor = 6,
            DataBaseDuplicateTerminal = 7,
            DataBaseDuplicateAcceptor = 8,
            DuplicateCustomerIdentifier = 9,
            InvalidCardNumber = 10,
            InvalidDateFormat = 11,
            InvalidAmount = 12,
            InvalidNationalCodeNumber = 13,
            InvalidCustomerNumber = 14,
            InvalidBirthCertificateNumber = 15,
            InvalidInputLength = 16,
            InvalidLatinInput = 17,
            RequiredInput = 18
        }
        public enum GroupAction
        {
            Festival = 1,
            Settlement = 2,
            Discount = 3,
            Fee = 4,
            Message = 5,
            PosVersion = 6,
            TerminalGroup = 7
        }

        public enum IssuerActionGroup
        {
            Customer = 1
        }

        public enum BatchStatus
        {
            Inserted = 0,
            Started = 1,
            ShaparakSuspend = 2,
            Done = 3
        }

        public enum BatchModeType
        {
            Insert = 0,
            Update = 1
        }

        public enum BatchBussinesType
        {
            Terminal = 0,
            CardAcceptor = 1,
            BankAccount = 2
        }

        public enum BatchRecordStatus
        {
            InseretedInDB = 0,
            MovedToFile = 1,
            Error = 2,
            ShaparakError = 3,
            Done = 4
        }

        public enum ISSErrorType
        {
            Ensedad = 1,
            Sodoor = 2,
            Faalsazi = 3,
            Tamdid = 4,
            RamzeAvval = 5,
            RamzeDovvom = 6,
            Charge = 7,
            Roonevesht = 8,
            TerminalGroupRel = 9,
            ActiveCard = 10,
            ChangeCardGroup = 11,
            Document = 12,
            ChangeSegmentState = 13,
            ChangeMobileNumber = 14,
            CashAccountNotFound = 15,
            NotificationError = 16,
            ChangeCardTerminalGroup = 17,
            ChangeAgency = 18,
            Inactive = 19,
            TransferBalanceAmount = 20,
            ChargeCardGroupAccount = 21,
            BothPass = 22
        }
        public enum ISSProductCode
        {
            Kharid = 1,
            Etebari = 2,
            Hadie = 23,
            MegaCard = 38
        }

        public enum FileGenPassword
        {
            Yeksan = 1,
            RamzeAval = 2,
            RamzeDovom = 3,
            Yeksan2318 = 4
        }

        public class CardOperationType
        {
            public static Guid Ensedad { get { return new Guid("FE1E9DD4-9FC8-49C3-9D77-2753D251538B"); } }
            public static Guid Tamdid { get { return new Guid("49CE9157-FA98-4033-97F2-EF555063549F"); } }
            public static Guid TamdidWithOutChange { get { return new Guid("95F37EFC-D014-428B-B40D-22119BB0EB55"); } }
            public static Guid SodorRonevesht { get { return new Guid("64FE2AA2-4D26-41A3-B95E-768D6AF3C9DC"); } }
            public static Guid RafeEnsedad { get { return new Guid("72B7A566-3C80-4E67-A9DF-FBF5FBEB57FB"); } }
            public static Guid RamzAval { get { return new Guid("ECE681A7-D532-49E5-AD25-79E3963B5C7C"); } }
            public static Guid RamzDovom { get { return new Guid("EEEB48D4-F25F-49D1-AB16-CCCBB96321B9"); } }
            public static Guid BothPass { get { return new Guid("D55639AE-9941-4F1B-8B66-1A608B6EB8FD"); } }
            public static Guid Sodoor { get { return new Guid("90400A07-E26B-4C86-9D7A-C514EDE6C596"); } }
            public static Guid Charge { get { return new Guid("1ECE91B9-DB8D-4D29-BF93-E1B3AEFD96E3"); } }
            public static Guid ActiveCard { get { return new Guid("AD839C6A-3429-41DF-810D-2F3C5F2A53CC"); } }
            public static Guid ChangeCardGroup { get { return new Guid("16E39657-DB96-4139-A33B-A44313DCD36D"); } }
            public static Guid ChangeSegmentState { get { return new Guid("23AE218F-556C-4C59-ABB2-3DD4613DD6DE"); } }
            public static Guid ChangeMobileNumber { get { return new Guid("CD5CBCC7-FA4C-4249-85F1-365137CA27CB"); } }
            public static Guid ChangeCardTerminalGroup { get { return new Guid("CCA4635C-5D71-4C69-82A4-8E6AD7B0E5E8"); } }
            public static Guid ChangeAgency { get { return new Guid("D828475D-7968-4B0B-BCE0-E7F59E171F24"); } }
            public static Guid Inactive { get { return new Guid("8A9D2733-7187-41B9-9F25-7B60ACCE0340"); } }
            public static Guid AssignParentCard { get { return new Guid("458D79FD-3BDA-4A3A-9E07-45C0814C4FDE"); } }
            public static Guid DeleteParentCard { get { return new Guid("D7100073-4BA8-4B74-9595-34314F3B9409"); } }
            //public static Guid FromShetab { get { return new Guid(""); } }
            //public static Guid Disable { get { return new Guid(""); } }
            //public static Guid Enable { get { return new Guid(""); } }
        }

        public enum CardOperationTypeEnum
        {
            Ensedad = 2,
            SodorMojadad = 3,
            SodorRonevesht = 4,
            UnSusspend = 5,
            RamzAval = 6,
            RamzDovom = 7,
            Sodor = 8,
            Charge = 9,
            ActiveCard = 10,
            ChangeCardGroup = 11,
            ChangeSegmentState = 12,
            ChangeMobileNumber = 13,
            ChangeCardTerminalGroup = 14,
            ChangeAgency = 15,
            Inactive = 16,
            TransferExpiredCardBalance = 21
        }

        public enum FileSuspendType
        {
            Suspend = 1,
            Activating = 2
        }

        public enum CardType
        {
            Purchase = 1,
            Gift = 2,
            MegaCard = 3,
            Virtual = 4,
            Credit = 5
        }

        public enum FestivalType
        {
            FestivalFixedStep = 1,
            FestivalFixedPrize = 2
        }
        public enum ChargeCardStatusCode
        {
            Available = 0,
            Reserved = 1,
            Sold = 2,
            DuplicatedSerialInFile = -3,
            DuplicatedPinInFile = -4,
            DuplicatedSerialInDB = -5,
            DuplicatedPinInDatabase = -6,
        }

        public enum ACQStatus
        {
            JobStarted = -2,
            FileCreating = 0,
            Done = 2,


        }

        public enum ACQUploadFile
        {
            InsertAcceptor = 1,
            UpdateAcceptor = 2,
            InsertTerminal = 3,
            UpdateTerminal = 4,

        }

        public enum ACQLastSynchStatus
        {
            Insert = -1,
            Update = -2,
            FilePendingInsert = -3,
            FilePendingUpdate = -4,
            FileTransfer = 1
        }
        public enum FileStatusCode
        {
            WaitingToLastConfirm = -8,
            WaitingToFirstConfirm = -7,
            CreatingFile = -6,
            Inprogress = -5,
            Variancing = -4,
            Error = -3,
            SendingToSwitch = -2,
            Upload = -1,
            Completed = 0,
            CompletedWithError = 1,
            ProssessingFile = 2,
            WaitingForOperation = 3,
            ProssessingOperation = 4,
            WaitigFetchData = 5,
            Offline = 6,
            WaitingToSend = 7
        }

        public enum FileDetailStatus
        {
            WaitingToProcess = 0,
            Error = 1,
            Done = 2
        }

        public enum UserActions
        {
            ChangePasswordIsMandatory = 1
        }

        public enum PartyNatureCode
        {
            Natural = 1,
            Legal = 0
        }

        public enum ChargeFormat
        {
            Seperator = 1,
            FixLength = 2
        }


        public enum SPRcode
        {
            Purchase = 0,
            Balance = 31,
            ThirdParty = 50

        }

        public enum TerminalTypeCode
        {
            Mobile = 5,
            POS = 14,
            Internet = 59,
            KIOSK = 37,
            IVR = 7,
            IKT = 43,
            ATM = 2,
            BOF = -1,
            BRC = 3
        }

        public enum ContactType
        {
            SMS = 1,
            Email = 2,
            Both = 3
        }

        public enum ACTBtachRecordStatus
        {
            InseretedInDB = 0,
            MovedToFile = 1,
            Error = 2
        }

        public enum BranchIssueRequest
        {
            Draft = 0,

        }

        public enum ChargeCardGroupAccount
        {
            Draft = 0,
            Done = 1,
            Deleted = 2
        }
        public enum ChargeCardGroupType
        {
            Charge = 0,
            Deposit = 1,
            Transfer = 2
        }

        public enum TxnFeeDetails
        {
            SwitchFeeDetails = 1,
            BackOfficeFeeDetails = 2
        }

        public enum DocumentRegisterType
        {
            ReverseDocument = 0,
            CardAndCashDeskDocument = 1,
            CashDeskAndCashDeskDocument = 2,
            CardAndCardDocument = 3,
            CashDeskAndCostCenterDocument = 4
        }

    }
}
