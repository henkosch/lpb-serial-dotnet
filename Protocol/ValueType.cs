using System;
namespace LpbSerialDotnet.Protocol
{
    public enum ValueType : byte
    {
        VT_BIT,               //  2 Byte - 1 enable 0x01 / value
        VT_BYTE,              //  2 Byte - 1 enable 0x01 / value
        VT_CLOSEDOPEN,        //  2 Byte - 1 enable 0x01 / 0=Offen 1=Geschlossen Choice
        VT_DAYS,              //  2 Byte - 1 enable 0x01 / day
        VT_ENUM,              //* 2 Byte - 1 enable 0x01 / value        Choice
        VT_GRADIENT_SHORT,    //  2 Byte - 1 enable / value min/K
        VT_HOURS_SHORT,       //  2 Byte - 1 enable 0x01 / hours        Int08
        VT_LPBADDR,           //* 2 Byte - 1 enable / adr/seg           READ-ONLY
        VT_MINUTES_SHORT,     //  2 Byte - 1 enable 0x06 / minutes      Int08S
        VT_MONTHS,            //  2 Byte - 1 enable 0x06 / months       Int08S
        VT_ONOFF,             //  2 Byte - 1 enable 0x01 / 0=Aus  1=An (auch 0xff=An)
        //  VT_MANUAUTO,          //  2 Byte - 1 enable 0x01 / 0=Automatisch  1=Manuell //FUJITSU
        //  VT_BLOCKEDREL,        //  2 Byte - 1 enable 0x01 / 0=Gesperrt  1=Freigegeben //FUJITSU
        VT_PERCENT,           //  2 Byte - 1 enable 0x06 / percent
        VT_PERCENT5,          //  2 Byte - 1 enable 0x01 / value/2
        VT_PRESSURE,          //  2 Byte - 1 enable 0x01 / bar/10.0     READ-ONLY
        VT_SECONDS_SHORT,     //  2 Byte - 1 enable / seconds
        VT_SECONDS_SHORT4,    //  2 Byte - 1 enable 0x01 / value/4 (signed?)
        VT_SECONDS_SHORT5,    //  2 Byte - 1 enable 0x01 / value/5 (signed?)
        VT_TEMP_SHORT,        //  2 Byte - 1 enable 0x01 / value
        VT_TEMP_SHORT5,       //  2 Byte - 1 enable 0x01 / value/2 (signed)
        VT_TEMP_SHORT5_US,    //  2 Byte - 1 enable 0x01 / value/2 (unsigned)
        VT_TEMP_SHORT64,      //  2 Byte - 1 enable 0x01 / value/64 (signed)
        VT_VOLTAGE,           //  2 Byte - 1 enable / volt z.B. 2.9V
        VT_VOLTAGEONOFF,      //  2 Byte - 1 enable / volt 0V (0x00) or 230V (0xFF)
        VT_WEEKDAY,           //  2 Byte - 1 enable 0x01 / weekday (1=Mo..7=So)
        VT_YESNO,             //  2 Byte - 1 enable 0x01 / 0=Nein 1=Ja (auch 0xff=Ja)
        VT_CURRENT,           //  3 Byte - 1 enable / value/100 uA
        VT_CURRENT1000,       //  3 Byte - 1 enable / value/1000 uA
        VT_DAYS_WORD,         //  3 Byte - 1 enable / day
        VT_ERRORCODE,         //  3 Byte - 1 enable / value READ-ONLY
        VT_FP1,               //  3 Byte - 1 enable / value/10 READ-ONLY
        VT_FP02,              //  3 Byte - 1 enable 0x01 / value/50
        VT_GRADIENT,          //  3 Byte - 1 enable / value min/K
        VT_INTEGRAL,          //  3 Byte - 1 enable / value Kmin
        VT_MONTHS_WORD,       //  3 Byte - 1 enable 0x06 / months
        VT_HOUR_MINUTES,      //  3 Byte - 1 enable 0x06 / hh mm
        VT_HOURS_WORD,        //  3 Byte - 1 enable 0x06 / hours
        VT_MINUTES_WORD,      //  3 Byte - 1 enable 0x06 / minutes
        VT_MINUTES_WORD10,    //  3 Byte - 1 enable 0x06 / minutes / 10
        VT_PERCENT_WORD1,     //  3 Byte - 1 enable / percent
        VT_PERCENT_WORD,      //  3 Byte - 1 enable / percent/2
        VT_PERCENT_100,       //  3 Byte - 1 enable / percent/100
        VT_POWER_WORD,        //  3 Byte - 1 enable / value/10 kW
        VT_ENERGY_WORD,       //  3 Byte - 1 enable / value/10 kWh
        VT_ENERGY_CONTENT,    //  3 Byte - 1 enable / value/10 kWh/m³
        VT_PRESSURE_WORD,     //  3 Byte - 1 enable / bar/10.0
        VT_PROPVAL,           //  3 Byte - 1 enable / value/16
        VT_SECONDS_WORD,      //  3 Byte - 1 enable / seconds
        VT_SECONDS_WORD5,     //  3 Byte - 1 enable / seconds / 2
        VT_SPEED,             //  3 Byte - 1 enable / value * 50 rpm
        VT_SPEED2,            //  3 Byte - 1 enable / rpm
        VT_TEMP,              //  3 Byte - 1 enable / value/64
        VT_TEMP_WORD,         //  3 Byte - 1 enable / value
        VT_TEMP_WORD5_US,     //  3 Byte - 1 enable / value / 2
        VT_LITERPERHOUR,      //  3 Byte - 1 enable / value
        VT_LITERPERMIN,       //  3 Byte - 1 enable / value / 10
        VT_UINT,              //  3 Byte - 1 enable 0x06 / value
        VT_UINT5,             //  3 Byte - 1 enable / value * 5
        VT_UINT10,            //  3 Byte - 1 enable / value / 10
        VT_SINT,              //  3 Byte - 1 enable 0x06 / value
        VT_SINT1000,          //  3 Byte - 1 enable value / 1000
        VT_PPS_TIME,          //  4 Byte
        VT_DWORD,             //  5 Byte - 1 enable 0x06 / value
        VT_HOURS,             //  5 Byte - 1 enable / seconds/3600
        VT_MINUTES,           //  5 Byte - 1 enable 0x01 / seconds/60
        VT_POWER,             //  5 Byte - 1 enable / value/10 kW
        VT_POWER100,          //  5 Byte - 1 enable / value/100 kW
        VT_ENERGY10,          //  5 Byte - 1 enable / value/10 kWh
        VT_ENERGY,            //  5 Byte - 1 enable / value/1 kWh
        VT_UINT100,           //  5 Byte - 1 enable / value / 100
        VT_DATETIME,          //* 9 Byte - 1 enable 0x01 / year+1900 month day weekday hour min sec
        VT_SUMMERPERIOD,      //* 9 Byte - no flag? 1 enable / byte 2/3 month/year
        VT_VACATIONPROG,      //* 9 Byte - 1 enable 0x06 / byte 2/3 month/year
        VT_TIMEPROG,          //*12 Byte - no flag / 1_ein 1_aus 2_ein 2_aus 3_ein 3_aus (jeweils SS:MM)
        VT_STRING,            //* x Byte - 1 enable / string
        VT_CUSTOM_ENUM,       //* x Byte - 1 Byte Position, 1 Byte Parameter-Wert, Space, Text
        VT_CUSTOM_BYTE,       //* x Byte - 1 Byte Position, 1 Byte Länge Parameter, Space (!) (nötig für Erkennung)
        VT_UNKNOWN
    }
}
