import React from 'react';

import {
    List, Spin,
} from 'antd';
import { Link } from 'react-router-dom';

export default ({ loading, list }) => (
    <List>
        {
            loading ?
                <Spin spinning={loading} /> :
                list.map((item) => (
                    <List.Item key={item.id}>
                        <List.Item.Meta title={<Link to="">{item.cardNumber}</Link>} />
                    </List.Item>
                ))
        }
    </List>
);