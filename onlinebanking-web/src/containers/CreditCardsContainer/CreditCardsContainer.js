import React from 'react';
import BaseContainer from '../BaseContainer';
import CreditCardsList from '../../components/CreditCardsList/CreditCardsList';

import { connect } from 'react-redux';
import { fetchCreditCards } from '../../actions/crediCards';

class CreditCardsContainer extends BaseContainer {

    componentDidMount() {
        this.props
            .fetchCreditCards()
            .catch(error => {
                if (error && error.response && error.response.data) {
                    this.setState({ errorMessage: error.response.data });
                }
            });
    }

    render() {
        const { loading, creditCards } = this.props.creditCardsState;

        return (
            <CreditCardsList
                match={this.props.match}
                loading={loading}
                list={creditCards} />
        )
    }
};

export default connect(
    state => ({
        creditCardsState: state.creditCards
    }),
    dispatch => ({
        fetchCreditCards: () => dispatch(fetchCreditCards())
    })
)(CreditCardsContainer);