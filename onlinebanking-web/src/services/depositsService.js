import { apiGet, apiPost } from '../api/api';

export default {
    getTepositTypes(currency) {
        return apiGet(`/api/deposits/depositTypes/${currency}`);
    },
    createDeposit(body) {
        return apiPost('/api/deposits/create', body);
    }
}