import React from 'react';
import visa from '../../images/visa.png';
import masterCard from '../../images/MasterCard.png';
import './CreditCardType.css';
import { Spin } from 'antd';

export default function creditCardType({ creditCard }) {
    if (!creditCard)
        return (<Spin />);
    if (creditCard && creditCard.type === 0)
        return (
            <img className="image" src={visa}></img>
        );

    return (
        <img className="image" src={masterCard}></img>
    );
}