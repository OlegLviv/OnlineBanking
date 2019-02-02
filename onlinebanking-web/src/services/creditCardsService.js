import { apiGet, apiPost } from '../api/api';

export default {
    getCreditCards() {
        return apiGet('/api/creditCards');
    },
    createOrder(body){
        return apiPost('/api/creditCards/createOrder', body);
    }
}