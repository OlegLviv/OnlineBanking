import creditCardsService from '../services/creditCardsService';

import { handleError } from './handleError';

export const FETCH_CREDIT_CARDS_REQUEST = 'FETCH_CREDIT_CARDS_REQUEST';
export const FETCH_CREDIT_CARDS_SUCCESS = 'FETCH_CREDIT_CARDS_SUCCESS';
export const FETCH_CREDIT_CARDS_FAILURE = 'FETCH_CREDIT_CARDS_FAILURE';

const fetchCreditCardsRequest = () => ({
    type: FETCH_CREDIT_CARDS_REQUEST
});

const fetchCreditCardsSuccess = creditCards => ({
    type: FETCH_CREDIT_CARDS_SUCCESS,
    creditCards
});

const fetchCredicCardsFailure = error => ({
    type: FETCH_CREDIT_CARDS_FAILURE,
    error
});

export const fetchCreditCards = () => async dispatch => {
    dispatch(fetchCreditCardsRequest());

    return creditCardsService
        .getCreditCards()
        .then(
            resp => dispatch(fetchCreditCardsSuccess(resp.data)),
            error => handleError(dispatch, error, fetchCredicCardsFailure)
        );
};