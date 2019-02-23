import { apiGet, apiPost, apiPut } from '../api/api';

export default {
    getCreditCards() {
        return apiGet('/api/creditCards');
    },
    getCreditCard(id) {
        return apiGet(`/api/creditCards/${id}`);
    },
    createOrder(body) {
        return apiPost('/api/creditCards/createOrder', body);
    },
    changePin(body) {
        return apiPut('/api/creditCards/changePin', body);
    },
    changeCreditLimit(body) {
        return apiPut('/api/creditCards/changeCreditLimit', body);
    },
    sendMoneyToCard(body){
        return apiPost('/api/creditCards/sendMoneyToCard', body);
    }
}