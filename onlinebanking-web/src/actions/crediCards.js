import creditCardsService from '../services/creditCardsService';

import { handleError } from './handleError';

export const FETCH_CREDIT_CARDS_REQUEST = 'FETCH_CREDIT_CARDS_REQUEST';
export const FETCH_CREDIT_CARDS_SUCCESS = 'FETCH_CREDIT_CARDS_SUCCESS';
export const FETCH_CREDIT_CARDS_FAILURE = 'FETCH_CREDIT_CARDS_FAILURE';

export const CREATE_ORDER_REQUEST = 'CREATE_ORDER_REQUEST';
export const CREATE_ORDER_SUCCESS = 'CREATE_ORDER_SUCCESS';
export const CREATE_ORDER_FAILURE = 'CREATE_ORDER_FAILURE';

export const FETCH_CREDIT_CARD_REQUEST = 'FETCH_CREDIT_CARD_REQUEST';
export const FETCH_CREDIT_CARD_SUCCESS = 'FETCH_CREDIT_CARD_SUCCESS';
export const FETCH_CREDIT_CARD_FAILURE = 'FETCH_CREDIT_CARD_FAILURE';

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

const createOrderRequest = () => ({
    type: CREATE_ORDER_REQUEST
});

const createOrderSuccess = creditCardOrder => ({
    type: CREATE_ORDER_SUCCESS,
    creditCardOrder
});

const createOrderFailure = error => ({
    type: CREATE_ORDER_FAILURE,
    error
});


export const createOrder = body => async dispatch => {
    dispatch(createOrderRequest());

    return creditCardsService
        .createOrder(body)
        .then(
            resp => dispatch(createOrderSuccess(resp.data)),
            error => handleError(dispatch, error, createOrderFailure)
        );
};

const fetchCreditCardRequest = () => ({
    type: FETCH_CREDIT_CARD_REQUEST
});

const fetchCReditCardSuccess = creditCard => ({
    type: FETCH_CREDIT_CARD_SUCCESS,
    creditCard
});

const fetchCreditCardFailure = error => ({
    type: FETCH_CREDIT_CARD_FAILURE,
    error
});

export const fetchCreditCard = id => dispatch => {
    dispatch(fetchCreditCardRequest());

    return creditCardsService
        .getCreditCard(id)
        .then(
            resp => dispatch(fetchCReditCardSuccess(resp.data)),
            error => handleError(dispatch, error, fetchCreditCardFailure)
        );
}