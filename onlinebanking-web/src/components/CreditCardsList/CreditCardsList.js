import React from 'react';
import { dateToExpireCard } from '../../utils/timeUtils';

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
                            title={<Link to={`/cab/${match.params.role}/credit-card/${item.id}`}>{item.cardNumber}</Link>}
                            description={dateToExpireCard(item.expired)} />
                    </List.Item>
                ))
        }
    </List>
);