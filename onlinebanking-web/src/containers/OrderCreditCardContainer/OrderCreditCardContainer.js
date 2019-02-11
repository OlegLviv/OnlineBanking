import React from 'react';
import OrderCreditCardForm from '../../components/OrderCreditCardForm/OrderCreditCardForm';
import BaseContainer from '../BaseContainer';

import { connect } from 'react-redux';
import { createOrder } from '../../actions/crediCards';

class OrderCreditCardContainer extends BaseContainer {

    onCreateOrder = body => this.props
        .createOrder(body)
        .then(() => this.setState({ successMessage: 'Order have been created success' }))
        .catch(err => {
            if (err && err.status === 400) {
                this.setState({ errorMessage: err.data });
            }
        });

    render() {
        return (<OrderCreditCardForm onCreateOrder={this.onCreateOrder}></OrderCreditCardForm>);
    }
}

export default connect(
    state => ({
        creditCardState: state.creditCards
    }),
    dispatch => ({
        createOrder: body => dispatch(createOrder(body))
    })
)(OrderCreditCardContainer);