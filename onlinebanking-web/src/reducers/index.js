import token from './token';
import users from './users';
import creditCards from './creditCards';

import { combineReducers } from 'redux';

export default combineReducers({ token, users, creditCards });