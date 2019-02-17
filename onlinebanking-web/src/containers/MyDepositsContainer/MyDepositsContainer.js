import React from 'react';
import BaseContainer from '../BaseContainer';

import { connect } from 'react-redux';
import { fetchDeposits } from '../../actions/deposits';
import { List } from 'antd';

class MyDepositsContainer extends BaseContainer {

    componentWillMount() {
        this.props.fetchMyDeposits();
    }

    renderListItem = item => (
        <List.Item>
            {item.depositType.name}
        </List.Item>
    );

    render() {
        const { deposits, loading } = this.props.depositsState;

        return <List
            className="m-r-l-10"
            bordered
            loading={loading}
            dataSource={deposits}
            renderItem={item => this.renderListItem(item)}
        />
    }
}

export default connect(
    state => ({
        depositsState: state.deposits
    }),
    dispatch => ({
        fetchMyDeposits: () => dispatch(fetchDeposits())
    })
)(MyDepositsContainer);

