import token from './token';
import users from './users';
import crediCards from './creditCards';

import { combineReducers } from 'redux';

export default combineReducers({ token, users, crediCards });