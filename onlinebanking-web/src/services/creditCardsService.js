import { apiGet, apiPost } from '../api/api';

export default {
    getCreditCards() {
        return apiGet('/api/creditCards');
    },
    getCreditCard(id) {
        return apiGet(`/api/creditCards/${id}`);
    },
    createOrder(body) {
        return apiPost('/api/creditCards/createOrder', body);
    }
}