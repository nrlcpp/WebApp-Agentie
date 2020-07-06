
export interface Reservation {
    Id: number;
    Sum: number;
    Location: string;
    AddedOn: Date;
    Currency: Currency;
    Type: Type;
    DepartureTime: Date;
    ArrivalTime: Date;
    Documents: boolean;
    Remarks: Remarks;
}
export enum Type {
    circuit = 0,
    stay = 1,
    accommodation = 2,
    transport = 3,
    others = 4
}

export enum Currency {
    EUR = 0,
    RON = 1,
    USD = 2
}

export interface Remarks {
    id: number;
    agent: string;
    content: string;
    importance: Importance;
}

enum Importance {
    hight = 0,
    medium = 1,
    lower = 2
}
