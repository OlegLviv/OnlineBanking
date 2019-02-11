import React from 'react';
import visa from '../../images/visa.png';
import masterCard from '../../images/MasterCard.png';
import './CreditCardType.css';
import classnames from 'classnames';

import { Spin } from 'antd';

export default function creditCardType({ creditCard, className }) {
    if (!creditCard)
        return (<Spin />);
    if (creditCard && creditCard.type === 0)
        return (
            <img className={classnames("image", className)} src={visa} alt=""></img>
        );

    return (
        <img className={classnames("image", className)} src={masterCard} alt=""></img>
    );
}