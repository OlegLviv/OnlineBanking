import {
    FETCH_CREDIT_CARDS_REQUEST,
    FETCH_CREDIT_CARDS_SUCCESS,
    FETCH_CREDIT_CARDS_FAILURE
} from '../actions/crediCards';

const defaultState = {
    creditCards: [],
    creditCard: null,
    loading: false,
    error: null
};

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_CREDIT_CARDS_REQUEST:
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

        case FETCH_CREDIT_CARDS_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}