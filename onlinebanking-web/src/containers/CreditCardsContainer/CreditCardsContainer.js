import React from 'react';
import BaseContainer from '../BaseContainer';

import { AccountContext } from '../../providers/AccountProvider';
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
        return (
            <AccountContext.Consumer>
                {(context) => (<div>{console.log(context)}</div>)}
            </AccountContext.Consumer>
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