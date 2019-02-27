import React from 'react';
import CreditCardType from '../../components/CreditCardType/CreditCardType';
import './CreditCardsList.css';

import { dateToExpireCard } from '../../utils/timeUtils';
import { normalizeCardNumber } from '../../utils/creditCardUtil';

import {
    List, Spin
} from 'antd';
import { Link } from 'react-router-dom';

export default ({ loading, list, match }) => (
    <List>
        {
            loading ?
                <Spin spinning={loading} /> :
                list.map((item) => (
                    <List.Item key={item.id}>
                        <List.Item.Meta
                            title={
                                <Link to={`/cab/${match.params.role}/credit-card/${item.id}`}>
                                    <div className="credit-card-item">
                                        <CreditCardType
                                            className="credit-card-item-type"
                                            creditCard={item} />
                                        <div>
                                            <div>
                                                {normalizeCardNumber(item.cardNumber)}
                                            </div>
                                            <small>
                                                {dateToExpireCard(item.expired)}
                                            </small>
                                        </div>
                                    </div>
                                </Link>
                            } />
                    </List.Item>
                ))
        }
    </List>
);