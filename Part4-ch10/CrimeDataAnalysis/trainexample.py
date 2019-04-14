# coding:utf-8
# from __future__ import absolute_import
# from __future__ import division
# from __future__ import print_function
# from os import path
import numpy as np
import sys
sys.path.append(r"C:/Users/He Sunshine/PycharmProjects/Neural Network/venv/Lib/site-packages")
import tensorflow as tf
#调用类
from tensorflow.contrib.timeseries.python.timeseries import estimators as ts_estimators
from tensorflow.contrib.timeseries.python.timeseries import model as ts_model
from tensorflow.contrib.timeseries.python.timeseries import  NumpyReader
from matplotlib.ticker import MultipleLocator, FormatStrFormatter
import matplotlib
# matplotlib.use("agg")
import matplotlib.pyplot as plt
# matplotlib.use('qt4agg')
# #指定默认字体
# matplotlib.rcParams['font.sans-serif'] = ['SimHei']
# matplotlib.rcParams['font.family']='sans-serif'
# #解决负号'-'显示为方块的问题
# matplotlib.rcParams['axes.unicode_minus'] = False


class _LSTMModel(ts_model.SequentialTimeSeriesModel):
  """A time series model-building example using an RNNCell."""

  def __init__(self, num_units, num_features, dtype=tf.float32):
    """Initialize/configure the model object.
    Note that we do not start graph building here. Rather, this object is a
    configurable factory for TensorFlow graphs which are run by an Estimator.
    Args:
      num_units: The number of units in the model's LSTMCell.
      num_features: The dimensionality of the time series (features per
        timestep).
      dtype: The floating point data type to use.
    """
    super(_LSTMModel, self).__init__(
        # Pre-register the metrics we'll be outputting (just a mean here).
        train_output_names=["mean"],
        predict_output_names=["mean"],
        num_features=num_features,
        dtype=dtype)
    self._num_units = num_units
    # Filled in by initialize_graph()
    self._lstm_cell = None
    self._lstm_cell_run = None
    self._predict_from_lstm_output = None

  def initialize_graph(self, input_statistics):
    """Save templates for components, which can then be used repeatedly.
    This method is called every time a new graph is created. It's safe to start
    adding ops to the current default graph here, but the graph should be
    constructed from scratch.
    Args:
      input_statistics: A math_utils.InputStatistics object.
    """
    super(_LSTMModel, self).initialize_graph(input_statistics=input_statistics)
    self._lstm_cell = tf.nn.rnn_cell.LSTMCell(num_units=self._num_units)
    # Create templates so we don't have to worry about variable reuse.
    self._lstm_cell_run = tf.make_template(
        name_="lstm_cell",
        func_=self._lstm_cell,
        create_scope_now_=True)
    # Transforms LSTM output into mean predictions.
    self._predict_from_lstm_output = tf.make_template(
        name_="predict_from_lstm_output",
        func_=lambda inputs: tf.layers.dense(inputs=inputs, units=self.num_features),
        create_scope_now_=True)

  def get_start_state(self):
    """Return initial state for the time series model."""
    return (
        # Keeps track of the time associated with this state for error checking.
        tf.zeros([], dtype=tf.int64),
        # The previous observation or prediction.
        tf.zeros([self.num_features], dtype=self.dtype),
        # The state of the RNNCell (batch dimension removed since this parent
        # class will broadcast).
        [tf.squeeze(state_element, axis=0)
         for state_element
         in self._lstm_cell.zero_state(batch_size=1, dtype=self.dtype)])

  def _transform(self, data):
    """Normalize data based on input statistics to encourage stable training."""
    mean, variance = self._input_statistics.overall_feature_moments
    return (data - mean) / variance

  def _de_transform(self, data):
    """Transform data back to the input scale."""
    mean, variance = self._input_statistics.overall_feature_moments
    return data * variance + mean

  def _filtering_step(self, current_times, current_values, state, predictions):
    """Update model state based on observations.
    Note that we don't do much here aside from computing a loss. In this case
    it's easier to update the RNN state in _prediction_step, since that covers
    running the RNN both on observations (from this method) and our own
    predictions. This distinction can be important for probabilistic models,
    where repeatedly predicting without filtering should lead to low-confidence
    predictions.
    Args:
      current_times: A [batch size] integer Tensor.
      current_values: A [batch size, self.num_features] floating point Tensor
        with new observations.
      state: The model's state tuple.
      predictions: The output of the previous `_prediction_step`.
    Returns:
      A tuple of new state and a predictions dictionary updated to include a
      loss (note that we could also return other measures of goodness of fit,
      although only "loss" will be optimized).
    """
    state_from_time, prediction, lstm_state = state
    with tf.control_dependencies(
            [tf.assert_equal(current_times, state_from_time)]):
      transformed_values = self._transform(current_values)
      # Use mean squared error across features for the loss.
      predictions["loss"] = tf.reduce_mean((prediction - transformed_values) ** 2, axis=-1)
      # Keep track of the new observation in model state. It won't be run
      # through the LSTM until the next _imputation_step.
      new_state_tuple = (current_times, transformed_values, lstm_state)
    return (new_state_tuple, predictions)

  def _prediction_step(self, current_times, state):
    """Advance the RNN state using a previous observation or prediction."""
    _, previous_observation_or_prediction, lstm_state = state
    lstm_output, new_lstm_state = self._lstm_cell_run(
        inputs=previous_observation_or_prediction, state=lstm_state)
    next_prediction = self._predict_from_lstm_output(lstm_output)
    new_state_tuple = (current_times, next_prediction, new_lstm_state)
    return new_state_tuple, {"mean": self._de_transform(next_prediction)}


  def _imputation_step(self, current_times, state):
    """Advance model state across a gap."""
    # Does not do anything special if we're jumping across a gap. More advanced
    # models, especially probabilistic ones, would want a special case that
    # depends on the gap size.
    return state

  def _exogenous_input_step(
          self, current_times, current_exogenous_regressors, state):
    """Update model state based on exogenous regressors."""
    raise NotImplementedError(
        "Exogenous inputs are not implemented for this example.")


if __name__ == '__main__':
  print("请选择您要预测的犯罪类型：")
  print("1.Alarm\n2.Arson\n3.Weapons Offense\n4.Breaking & Entering\n5.Vehicle Stop\n6.Theft\n"
        "7.Sexual Assault\n8.Property Crime\n9.Drugs\n10.Liquor\n11.Others")
  num = input();
  num=int(num)
  tf.logging.set_verbosity(tf.logging.INFO)

  #输入训练数据
  x =[2001,2002,2003,2004,2005,2006,2007,2008,2009,2010., 2011, 2012, 2013,2014, 2015, 2016, 2017]
  if num==1:
      str="Alarm"
      y=[5924,4241,3852,2911,691,509,555,498., 469., 1556., 2517., 2501., 2842., 2959., 2490., 2296, 118] #Alarm
  if num==2:
      str="Arson"
      y=[27, 23, 15, 24, 18, 26, 18, 38, 35, 40, 56, 58, 73, 69, 59, 66, 84] #Arson
  if num==3:
      str="Weapons Offense"
      y=[153, 145, 124, 130, 142, 175, 160, 174, 156, 239, 403, 339, 300, 399, 498, 490, 486] #Weapons Offense
  if num==4:
      str="Breaking & Entering"
      y=[952,1006,1082,1035,1112,1423,1429,1314,1571,1812,2479,2653,2348,3819,3671,3450,2897] #Breaking & Entering
  if num==5:
      str="Vehicle Stop"
      y=[840,5990,1084,1178,1111,1420,1495,1195,1014,3218,5858,5091,7079,6996,6238,5195,4038]#Vehicle Stop
  if num==6:
      str="Theft"
      y=[2751,2639,2753,2700,2712,2877,2780,3327,4222,5427,7115,7252,7160,9500,12205,11544,9952]#Theft
  if num==7:
      str="Sexual Assault"
      y=[20,28,28,28,28,26,33,37,42,59,69,48,64,74,66,64,57]#Sexual Assault
  if num==8:
      str="Property Crime"
      y=[32,43,33,34,17,17,17,297,550,1053,1692,1602,1510,2961,3900,3989,3621]
  if num==9:
      str="Drugs"
      y=[764,928,979,845,1083,1173,1183,1086,954,1303,1809,1793,1756,3043,2709,2471,2361]
  if num==10:
      str="Liquor"
      y=[887,745,682,596,706,689,852,941,1104,1249,2007,1823,1675,2283,2112,1561,1351]
  if num==11:
      str="Others"
      y=[4,1,0,0,0,2,10,318,1227,6522,12697,13363,13884,15657,14782,14131,12819]
  #把x和y变成python中的词典（变量data）
  data = {
      tf.contrib.timeseries.TrainEvalFeatures.TIMES: x,
      tf.contrib.timeseries.TrainEvalFeatures.VALUES: y,
  }#也直接写成data = {‘times’:x, ‘values’:y}
  reader = NumpyReader(data)

#tf.contrib.timeseries.RandomWindowInputFn会在reader的所有数据中，随机选取窗口长度为window_size的序列，
  # 并包装成batch_size大小的batch数据。
  # 换句话说，一个batch内共有batch_size个序列，每个序列的长度为window_size。
  #这里我用的是full batch size(全数据集)
  train_input_fn = tf.contrib.timeseries.RandomWindowInputFn(
      reader, batch_size=17, window_size=17)
#打印出batch 里的数据
  with tf.Session() as sess:
      batch_data = train_input_fn.create_batch()
      coord = tf.train.Coordinator()
      threads = tf.train.start_queue_runners(sess=sess, coord=coord)
      one_batch = sess.run(batch_data[0])
      coord.request_stop()
  print('one_batch_data:', one_batch)

  # # num_features = 1,表示单变量时间序列，即每个时间点上观察到的量只是一个单独的数值。
  # num_units = 128表示使用隐层为128大小的LSTM模型
  estimator = ts_estimators.TimeSeriesRegressor(
      model=_LSTMModel(num_features=1, num_units=128),
      optimizer=tf.train.AdamOptimizer(0.006))

  # 训练、验证和预测的方法都和之前类似。在训练时,在已有的观察量的基础上向后预测5步：
  estimator.train(input_fn=train_input_fn, steps=4000)  #训练的步数
  evaluation_input_fn = tf.contrib.timeseries.WholeDatasetInputFn(reader)
  evaluation = estimator.evaluate(input_fn=evaluation_input_fn, steps=1)
  # Predict starting after the evaluation
  (predictions,) = tuple(estimator.predict(
      input_fn=tf.contrib.timeseries.predict_continuation_input_fn(
          evaluation, steps=5)))#预测未来五年的效果比较好

  # def _filtering_step(self, current_times, current_values, state, predictions):
  #   state_from_time, prediction, lstm_state = state
  #   with tf.control_dependencies(
  #           [tf.assert_equal(current_times, state_from_time)]):
  #     transformed_values = self._transform(current_values)
  #     # Use mean squared error across features for the loss.
  #     predictions["loss"] = tf.reduce_mean((prediction - transformed_values) ** 2, axis=-1)
  #     lossline = predictions["loss"]
  #     plt.figure(figsize=(15,5))
  #     loss_lines = plt.plot(np.array(range(1000)),lossline, label="real data", color="k")

  observed_times = evaluation["times"][0]
  observed = evaluation["observed"][0, :, :]
  evaluated_times = evaluation["times"][0]
  evaluated = evaluation["mean"][0]
  predicted_times = predictions['times']
  predicted = predictions["mean"]



  plt.figure(figsize=(15, 5))#图片的大小
#  the prediction of crime events
  plt.title("the prediction of CA_%s crime events (up to 2022 year)"%(str))
  plt.axvline(2017, linestyle="dotted", linewidth=2, color='r')
  #plt.plot(x, y, linewidth = '1', label = "test", color=' coral ', linestyle=':', marker='|')
  observed_lines = plt.plot(observed_times, observed, label="real data",color="k")
  evaluated_lines = plt.plot(evaluated_times, evaluated, label="evaluation", linestyle='-.',color="g")
  predicted_lines = plt.plot(predicted_times, predicted, label="prediction", linestyle='-',color="r")
  plt.legend(handles=[observed_lines[0], evaluated_lines[0], predicted_lines[0]],
             loc="upper left")
  plt.xlim(2000,2025) #x坐标轴的刻度范围
  # numpy.linspace()方法返回一个等差数列数组,第一个参数表示等差数列的第一个数，第二个参数表示等差数列最后一个数，第三个参数设置组成等差数列的元素个数，endpoint参数设置最后一个数是否包含在该等差数列。数列中相邻元素间的步长值为随机
  # 如：nu.linspace(0, 1000, 15, endpoint=True)
  # 表示：第一个元素为0，最后一个数为1000，在这个
  # 范围内，取15个值，构成一个等差数列，步长值随机，且1000包含在该数列中
  # plt.xticks(np.linspace(2000, 2026,10, endpoint=True))
  plt.xlabel('time(year)')
  plt.ylabel('number')
  plt.grid(True)
  # plt.xticks(fontsize=18)
  # plt.yticks(fontsize=18)
  plt.show();
  #保存图片
  #plt.savefig('predict_result1.png')
