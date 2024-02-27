﻿namespace DebtManager.Domain;

public enum DealStatusType
{
    LenderApproved,
    BorrowerApproved,
    Opened,

    LenderCanceled,
    BorrowerCanceled,
    Canceled,

    LenderClosed,
    BorrowerClosed,
    Closed
}