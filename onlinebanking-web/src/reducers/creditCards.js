import {
    FETCH_CREDIT_CARDS_REQUEST,
    FETCH_CREDIT_CARDS_SUCCESS,
    FETCH_CREDIT_CARDS_FAILURE,

    FETCH_CREDIT_CARD_REQUEST,
    FETCH_CREDIT_CARD_SUCCESS,
    FETCH_CREDIT_CARD_FAILURE,

    FETCH_COSTS_LOG_REQUEST,
    FETCH_COSTS_LOG_SUCCESS,
    FETCH_COSTS_LOG_FAILURE,

    CREATE_ORDER_REQUEST,
    CREATE_ORDER_SUCCESS,
    CREATE_ORDER_FAILURE,

    CHANGE_PIN_REQUEST,
    CHANGE_PIN_SUCCESS,
    CHANGE_PIN_FAILURE,

    CHANGE_CREDIT_LIMIT_REQUEST,
    CHANGE_CREDIT_LIMIT_SUCCESS,
    CHANGE_CREDIT_LIMIT_FAILURE,

    SEND_MONEY_TO_CARD_REQUEST,
    SEND_MONEY_TO_CARD_SUCCESS,
    SEND_MONEY_TO_CARD_FAILURE
} from '../actions/crediCards';

const defaultState = {
    creditCards: [],
    creditCard: null,
    loading: false,
    error: null,
    creditCardOrder: null,
    costsLog: null
};

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_CREDIT_CARDS_REQUEST:
        case CREATE_ORDER_REQUEST:
        case FETCH_CREDIT_CARD_REQUEST:
        case CHANGE_PIN_REQUEST:
        case CHANGE_CREDIT_LIMIT_REQUEST:
        case SEND_MONEY_TO_CARD_REQUEST:
        case FETCH_COSTS_LOG_REQUEST:
            return {
                ...state,
                loading: true
            };

        case FETCH_CREDIT_CARDS_SUCCESS:
            return {
                ...state,
                loading: false,
                creditCards: action.creditCards
            };

        case FETCH_CREDIT_CARD_SUCCESS:
        case CHANGE_PIN_SUCCESS:
        case CHANGE_CREDIT_LIMIT_SUCCESS:
        case SEND_MONEY_TO_CARD_SUCCESS:
            return {
                ...state,
                loading: false,
                creditCard: action.creditCard
            };
        case CREATE_ORDER_SUCCESS:
            return {
                ...state,
                loading: false,
                creditCardOrder: action.creditCardOrder
            };
        case FETCH_COSTS_LOG_SUCCESS:
            return {
                ...state,
                loading: false,
                costsLog: action.costsLog
            };

        case FETCH_CREDIT_CARDS_FAILURE:
        case CREATE_ORDER_FAILURE:
        case FETCH_CREDIT_CARD_FAILURE:
        case CHANGE_PIN_FAILURE:
        case CHANGE_CREDIT_LIMIT_FAILURE:
        case SEND_MONEY_TO_CARD_FAILURE:
        case FETCH_COSTS_LOG_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}