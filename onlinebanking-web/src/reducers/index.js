import token from './token';
import users from './users';
import creditCards from './creditCards';
import deposits from './deposits';

import { combineReducers } from 'redux';

export default combineReducers({
    token,
    users,
    creditCards,
    deposits
});