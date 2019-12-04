import {combineReducers} from "redux";
import {routerReducer } from 'react-router-redux';

import {i18nReducer} from "../../i18n/store/reducer";

export const createReducers = () => {
    return combineReducers({
        routing: routerReducer,
        i18n: i18nReducer
    })
};