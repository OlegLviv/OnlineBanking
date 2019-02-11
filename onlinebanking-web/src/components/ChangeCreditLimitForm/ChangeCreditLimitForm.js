import React from 'react';

import {
    Form, Input, Button
} from 'antd';

const onSend = (onSendChangeCreditLimit, form, event) => {
    event.preventDefault();

    form
        .validateFields()
        .then(e => onSendChangeCreditLimit(e));
}

const changeCreditLimitForm = ({ form, onSendChangeCreditLimit }) => (
    <Form style={{ padding: '2rem 30%' }} onSubmit={e => onSend(onSendChangeCreditLimit, form, e)}>
        <Form.Item>
            {form.getFieldDecorator('newLimit', {
                rules: [{
                    required: true,
                    message: 'Please input correct new value. Max credit limit is 100 000',
                    pattern: /^[0-9]{1,6}$/,
                    max: 100000,
                    min: 0
                }]
            })(
                <Input type="number" placeholder="Input new credit limit..." />
            )}
        </Form.Item>
        <Form.Item>
            <Button type="primary" htmlType="submit">
                Change
        </Button>
        </Form.Item>
    </Form>
);

export default Form.create()(changeCreditLimitForm);