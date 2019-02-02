import {
    FETCH_CREDIT_CARDS_REQUEST,
    FETCH_CREDIT_CARDS_SUCCESS,
    FETCH_CREDIT_CARDS_FAILURE,

    CREATE_ORDER_REQUEST,
    CREATE_ORDER_SUCCESS,
    CREATE_ORDER_FAILURE
} from '../actions/crediCards';

const defaultState = {
    creditCards: [],
    creditCard: null,
    loading: false,
    error: null,
    creditCardOrder: null
};

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_CREDIT_CARDS_REQUEST:
        case CREATE_ORDER_REQUEST:
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
        case CREATE_ORDER_SUCCESS:
            return {
                ...state,
                loading: false,
                creditCardOrder: action.creditCardOrder
            };

        case FETCH_CREDIT_CARDS_FAILURE:
        case CREATE_ORDER_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}