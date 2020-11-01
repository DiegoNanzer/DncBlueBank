import { IAccountModel } from "./Account";

export interface IAccountTransaction {

    id: number,
    AccountID: number,
    type: eTransaction,
    value: number,
    dateTime: Date,
}

export enum eTransaction {
    "DEPOSIT" = 1,
    "WITHDRAW" = 2,
}