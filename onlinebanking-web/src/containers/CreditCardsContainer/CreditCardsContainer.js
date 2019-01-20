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
        console.log(this.props)
        const { loading, creditCards } = this.props.creditCardsState;

        return (
            <CreditCardsList
                loading={loading}
                list={[
                    {
                        id:24,
                        cardNumber: '6445-456456-456-456'
                    },
                    {
                        id:456,
                        cardNumber: '6445-456456-456-456'
                    },
                    {
                        id:4,
                        cardNumber: '6445-456456-456-456'
                    },{
                        id:34,
                        cardNumber: '6445-456456-456-456'
                    }
                ]} />
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