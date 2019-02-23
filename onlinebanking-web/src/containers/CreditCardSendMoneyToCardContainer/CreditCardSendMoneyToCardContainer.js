import React from 'react';
import BaseContainer from '../BaseContainer';
import SendMoneyToCardForm from '../../components/SendMoneyToCardForm/SendMoneyToCardForm';

import { connect } from 'react-redux';
import { sendMoneyToCard } from '../../actions/crediCards';

class CreditCardSendMoneyToCardContainer extends BaseContainer {

    onSendMoneySubmit = body => {
        body.fromCardId = this.props
            .match
            .params
            .id;

        this.props
            .sendMoneyToCard(body)
            .then(() => this.setState({ successMessage: 'Money was sent successfully' }))
            .catch(er => {
                if (er && er.status && er.status === 400)
                    this.setState({ errorMessage: er.data });
            })
    }

    render() {
        return <SendMoneyToCardForm onSendMoneySubmit={this.onSendMoneySubmit} />;
    }
}

export default connect(
    state => ({
        creditCardState: state.creditCards
    }),
    dispatch => ({
        sendMoneyToCard: body => dispatch(sendMoneyToCard(body))
    })
)(CreditCardSendMoneyToCardContainer);