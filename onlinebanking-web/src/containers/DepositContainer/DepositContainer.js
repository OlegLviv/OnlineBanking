import React from 'react';
import BaseContainer from '../BaseContainer';

import { connect } from 'react-redux';
import { fetchDepositTypes } from '../../actions/deposits';

const getCurrentCurrencyFromUrlParams = props => props
    .match
    .params
    .currency;

class DepositContainer extends BaseContainer {

componentWillMount(){
    this.props.fetchDepositTypes(getCurrentCurrencyFromUrlParams(this.props));
}

    render() {
        // console.log('cur',getCurrentCurrencyFromUrlParams(this.props));
        return <div>Dep cont</div>
    }
}

export default connect(
    state => ({
        depositState: state.deposits
    }),
    dispatch => ({
        fetchDepositTypes: currency => dispatch(fetchDepositTypes(currency))
    })
)(DepositContainer);