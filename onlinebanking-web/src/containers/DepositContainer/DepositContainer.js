import React from 'react';
import BaseContainer from '../BaseContainer';
import "./DepositContainer.css";

import { connect } from 'react-redux';
import { fetchDepositTypes, createDeposit } from '../../actions/deposits';
import { Collapse, List, Button } from 'antd';


const getCurrentCurrencyFromUrlParams = props => props
    .match
    .params
    .currency;

class DepositContainer extends BaseContainer {

    componentWillMount() {
        this
            .props
            .fetchDepositTypes(getCurrentCurrencyFromUrlParams(this.props));
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.match && getCurrentCurrencyFromUrlParams(this.props) !== getCurrentCurrencyFromUrlParams(nextProps)) {
            this.props.fetchDepositTypes(getCurrentCurrencyFromUrlParams(nextProps));
        }
    }

    onOpenDepositClick = depositTypeId => this.props
        .createDeposit({ depositTypeId })
        .then(() => {
            this.setState({ successMessage: 'Deposit was successfully opened' });
        })
        .catch(err => {
            if (err && err.status && err.status === 400)
                this.setState({ errorMessage: err.data });
        });

    render() {
        const { depositTypes, loading } = this.props.depositState;

        return (
            <List
                className="p-r-l-10"
                dataSource={depositTypes}
                loading={loading}
                renderItem={(item, i) => (
                    <List.Item >
                        <Collapse className="w-100" defaultActiveKey={['1']}>
                            <Collapse.Panel header={item.name} key={i + 1}>
                                <div>About</div>
                                <div className="about-box">
                                    <div>
                                        <div className="f-bold">{`${item.percentages}%`}</div>
                                        <div>Per year</div>
                                    </div>
                                    <div>
                                        <div className="f-bold">{item.months}</div>
                                        <div>Months</div>
                                    </div>
                                    <div>
                                        <div className="f-bold">{item.currency}</div>
                                        <div>Currency</div>
                                    </div>
                                </div>
                                <Button 
                                type="primary" 
                                onClick={() => this.onOpenDepositClick(item.id)}
                                disabled={item.isTaken}>
                                    {item.isTaken ? 'Deposit already opened' : 'Open deposit'}
                                </Button>
                            </Collapse.Panel>
                        </Collapse>
                    </List.Item>
                )} />
        );
    }
}

export default connect(
    state => ({
        depositState: state.deposits
    }),
    dispatch => ({
        fetchDepositTypes: currency => dispatch(fetchDepositTypes(currency)),
        createDeposit: body => dispatch(createDeposit(body))
    })
)(DepositContainer);